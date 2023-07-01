namespace WebSnake.Modules.FakeNetworking
{
    public class FakeTransporter : ME.ECS.Network.ITransporter
    {
        private struct Buffer
        {
            public byte[] data;
        }

        private readonly System.Collections.Generic.Queue<Buffer> buffers = new();
        private readonly ME.ECS.Network.NetworkType networkType;
        private int sentCount;
        private int sentBytesCount;
        private int receivedCount;
        private int receivedBytesCount;
        private double ping;

        public FakeTransporter(ME.ECS.Network.NetworkType networkType)
        {
            this.networkType = networkType;
        }

        public bool IsConnected()
        {
            return true;
        }

        public void SendSystem(byte[] bytes)
        {
            this.Send(bytes);
        }

        public void Send(byte[] bytes)
        {
            if ((this.networkType & ME.ECS.Network.NetworkType.RunLocal) == 0)
            {
                // Add to local buffer if RunLocal flag is not set.
                // If flag RunLocal is set, this event has been run already.
                // This is FakeTransporter behaviour only.
                this.AddToBuffer(bytes);
            }

            // TODO: Here you need to send bytes array via your real transport layer to test real network environment.

            this.sentBytesCount += bytes.Length;
            ++this.sentCount;
        }

        private void AddToBuffer(byte[] bytes)
        {
            this.buffers.Enqueue(new Buffer()
            {
                data = bytes,
            });
        }

        private Buffer currentBuffer;

        public byte[] Receive()
        {
            // This method run every tick and should return data from network
            // byte[] array will be deserialized by ISerializer into HistoryEvent

            if (this.currentBuffer.data != null && this.currentBuffer.data.Length > 0)
            {
                this.receivedBytesCount += this.currentBuffer.data.Length;
                ++this.receivedCount;
                return this.currentBuffer.data;
            }

            if (this.buffers.Count > 0)
            {
                var buffer = this.buffers.Dequeue();
                this.currentBuffer = buffer;
            }

            return null;
        }

        public int GetEventsSentCount()
        {
            return this.sentCount;
        }

        public int GetEventsBytesSentCount()
        {
            return this.sentBytesCount;
        }

        public int GetEventsReceivedCount()
        {
            return this.receivedCount;
        }

        public int GetEventsBytesReceivedCount()
        {
            return this.receivedBytesCount;
        }
    }
}