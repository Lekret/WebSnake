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
    public class WebSocketWrapper : IDisposable
    {
        private readonly CancellationTokenSource _cts = new();
        private readonly ClientWebSocket _webSocket = new();
        private readonly List<object> _responses = new();

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

            var byteArray = ArrayPool<byte>.Shared.Rent(8192);

            try
            {
                var json = JsonConvert.SerializeObject(data);
                var bytes = Encoding.UTF8.GetBytes(json);
                await _webSocket.SendAsync(
                    new ArraySegment<byte>(bytes),
                    WebSocketMessageType.Text,
                    true,
                    _cts.Token);

                var buffer = new ArraySegment<byte>(byteArray);
                using var ms = new MemoryStream();
                WebSocketReceiveResult result;
                do
                {
                    result = await _webSocket.ReceiveAsync(byteArray, _cts.Token);
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

                    _responses.Add(JsonConvert.DeserializeObject(response, responseType));
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
                ArrayPool<byte>.Shared.Return(byteArray);
            }
        }

        public bool TryRead<T>(out T data)
        {
            for (var i = 0; i < _responses.Count; i++)
            {
                if (_responses[i].GetType() != typeof(T))
                    continue;

                data = (T) _responses[i];
                _responses.RemoveAt(i);
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
    }
}