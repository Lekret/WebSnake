using System;
using ME.ECS;
using ME.ECS.Collections.LowLevel.Unsafe;
using WebSnake.Web;

namespace WebSnake.Components
{
    public struct GameWebSocket : IStructCopyable<GameWebSocket>, IComponentDisposable<GameWebSocket>
    {
        public WebSocketWrapper Value;

        public void CopyFrom(in GameWebSocket other) => Value = other.Value;

        public void OnRecycle() => Value = null;
        
        public void OnDispose(ref MemoryAllocator allocator)
        {
            Value.Dispose();
        }

        public void ReplaceWith(ref MemoryAllocator allocator, in GameWebSocket other)
        {
            Value.Dispose();
            Value = other.Value;
        }
    }

    public struct GameId : IComponentShared
    {
        public int Value;
    }

    public struct CollectedApplesCount : IComponentShared
    {
        public int Value;
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

    public struct ReceivedResponse : IComponentOneShot
    {
        public string Value;
    }

    public struct GenerateGrid : IComponentOneShot
    {
        public int Width;
        public int Height;
    }

    public struct GameLoaded : IComponentShared
    {
        
    }

    public struct GameLaunched : IStructComponent, IComponentShared
    {
        
    }
}