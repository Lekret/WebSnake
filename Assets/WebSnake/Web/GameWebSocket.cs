using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ME.ECS.Buffers;
using Newtonsoft.Json;
using UnityEngine;

namespace WebSnake.Web
{
    public class GameWebSocket : IGameWebSocket
    {
        private readonly CancellationTokenSource _cts = new();
        private readonly ClientWebSocket _webSocket = new();
        private readonly Dictionary<Type, Queue<object>> _responsesMap = new();

        public async void Connect(string uri)
        {
            await _webSocket.ConnectAsync(new Uri(uri), _cts.Token);
        }

        public async void SendData(object data, Type responseType)
        {
            while (_webSocket.State == WebSocketState.Connecting)
            {
                await Task.Yield();
            }

            var requestBytes = ArrayPool<byte>.Shared.Rent(8192);
            var responseBytes = ArrayPool<byte>.Shared.Rent(8192);

            try
            {
                var request = JsonConvert.SerializeObject(data);
#if UNITY_EDITOR
                Debug.Log($"Request: {request}");
#endif
                var requestBytesCount = Encoding.UTF8.GetBytes(request, requestBytes);
                await _webSocket.SendAsync(
                    new ArraySegment<byte>(requestBytes, 0, requestBytesCount),
                    WebSocketMessageType.Text,
                    true,
                    _cts.Token);

                var buffer = new ArraySegment<byte>(responseBytes);
                using var ms = new MemoryStream();
                WebSocketReceiveResult result;
                
                do
                {
                    result = await _webSocket.ReceiveAsync(responseBytes, _cts.Token);
                    ms.Write(buffer.Array, buffer.Offset, result.Count);
                } while (!result.EndOfMessage);

                ms.Seek(0, SeekOrigin.Begin);

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    using var reader = new StreamReader(ms, Encoding.UTF8);
                    var response = await reader.ReadLineAsync();
                    if (response.Contains("error"))
                    {
                        Debug.LogError($"Invalid response: {response}");
                        return;
                    }

#if UNITY_EDITOR
                    Debug.Log($"Response: {response}");
#endif
                    var responses = GetResponsesOfType(responseType);
                    responses.Enqueue(JsonConvert.DeserializeObject(response, responseType));
                }
                else
                {
                    Debug.LogError($"Unhandled socket message type: {result.MessageType}");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Unhandled exception: {e}");
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(requestBytes);
                ArrayPool<byte>.Shared.Return(responseBytes);
            }
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
            _cts?.Dispose();
            _webSocket?.Dispose();
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