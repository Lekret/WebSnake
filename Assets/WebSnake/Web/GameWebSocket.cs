using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityWebSocket;
using ErrorEventArgs = UnityWebSocket.ErrorEventArgs;
using WebSocket = UnityWebSocket.WebSocket;

namespace WebSnake.Web
{
    public class GameWebSocket : IGameWebSocket
    {
        private readonly WebSocket _webSocket;
        private readonly Queue<Type> _awaitingResponses = new();
        private readonly Dictionary<Type, Queue<object>> _responsesMap = new();

        public GameWebSocket(string address)
        {
            _webSocket = new WebSocket(address);
        }
        
        public void Connect()
        {
            _webSocket.OnOpen += OnOpen;
            _webSocket.OnClose += OnClose;
            _webSocket.OnError += OnError;
            _webSocket.OnMessage += OnMessage;
            _webSocket.ConnectAsync();
        }

        private static void OnError(object sender, ErrorEventArgs e)
        {
            Debug.LogError($"WebSocketError: {e.Exception}");
        }

        private static void OnClose(object sender, CloseEventArgs e)
        {
            Debug.Log($"WebSocket closed: {e.Reason}");
        }

        private static void OnOpen(object sender, OpenEventArgs e)
        {
            Debug.Log("WebSocket opened");
        }

        private void OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Data.Contains("error"))
            {
                Debug.LogError($"Error response: {e.Data}");
                return;
            }

            Debug.Log($"Response: {e.Data}");

            if (_awaitingResponses.Count == 0)
            {
                Debug.LogError("No awaiting responses");
                return;
            }
            
            Type responseType;
            while (_awaitingResponses.TryDequeue(out responseType))
            {
                if (responseType != null)
                    break;
            }
  
            if (responseType == null)
                return;
            
            var responses = GetResponsesOfType(responseType);
            var responseObj = JsonConvert.DeserializeObject(e.Data, responseType);
            if (responseObj is null)
            {
                Debug.LogError($"Cannot deserialize response to type: {responseType}");
                return;
            }
            
            responses.Enqueue(responseObj);
        }

        public async void SendData(object data, Type responseType)
        {
            if (_webSocket.ReadyState == WebSocketState.Closing ||
                _webSocket.ReadyState == WebSocketState.Closed)
            {
                Debug.LogError("WebSocket is closing/closed");
                return;
            }
            
            while (_webSocket.ReadyState == WebSocketState.Connecting)
            {
                await Task.Yield();
            }

            var request = JsonConvert.SerializeObject(data);
            Debug.Log($"Request: {request}");
            _awaitingResponses.Enqueue(responseType);
            _webSocket.SendAsync(request);
        }

        public bool TryRead<T>(out T data)
        {
            var responses = GetResponsesOfType(typeof(T));
            if (responses.TryDequeue(out var rawData))
            {
                data = (T) rawData;
                return true;
            }

            data = default;
            return false;
        }

        public void Dispose()
        {
            _webSocket.CloseAsync();
        }

        private Queue<object> GetResponsesOfType(Type type)
        {
            if (!_responsesMap.TryGetValue(type, out var result))
            {
                result = new Queue<object>();
                _responsesMap.Add(type, result);
            }

            return result;
        }
    }
}