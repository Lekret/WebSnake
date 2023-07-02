using System;

namespace WebSnake.Web
{
    public interface IGameWebSocket : IDisposable
    {
        void Connect();
        void SendData(object data, Type responseType);
        bool TryRead<T>(out T data);
    }
}