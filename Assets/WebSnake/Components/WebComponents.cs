using System;
using ME.ECS;
using ME.ECS.Collections.LowLevel.Unsafe;
using WebSnake.Web;

namespace WebSnake.Components
{
    public struct GameWebSocketHolder :
        IStructCopyable<GameWebSocketHolder>,
        IComponentDisposable<GameWebSocketHolder>
    {
        public IGameWebSocket Value;

        public void CopyFrom(in GameWebSocketHolder other) => Value = other.Value;

        public void OnRecycle() => Value = null;

        public void OnDispose(ref MemoryAllocator allocator) => Value.Dispose();

        public void ReplaceWith(ref MemoryAllocator allocator, in GameWebSocketHolder other)
        {
            Value.Dispose();
            Value = other.Value;
        }
    }

    public struct SendRequest : IStructCopyable<SendRequest>
    {
        public object Data;
        public Type ResponseType;

        public void CopyFrom(in SendRequest other)
        {
            Data = other.Data;
            ResponseType = other.ResponseType;
        }

        public void OnRecycle()
        {
            Data = null;
            ResponseType = null;
        }
    }

    public struct EndGameResponseHolder : IStructCopyable<EndGameResponseHolder>, IComponentShared 
    {
        public EndGameResponse Value;
        
        public void CopyFrom(in EndGameResponseHolder other) => Value = other.Value;

        public void OnRecycle() => Value = null;
    }
}