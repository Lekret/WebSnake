namespace WebSnake.Modules.FakeNetworking
{
    public class FSSerializer : ME.ECS.Network.ISerializer
    {
        public byte[] SerializeStorage(ME.ECS.StatesHistory.HistoryStorage historyEvent)
        {
            return ME.ECS.Serializer.Serializer.Pack(historyEvent);
        }

        public ME.ECS.StatesHistory.HistoryStorage DeserializeStorage(byte[] bytes)
        {
            return ME.ECS.Serializer.Serializer.Unpack<ME.ECS.StatesHistory.HistoryStorage>(bytes);
        }

        public byte[] Serialize(ME.ECS.StatesHistory.HistoryEvent historyEvent)
        {
            return ME.ECS.Serializer.Serializer.Pack(historyEvent);
        }

        public ME.ECS.StatesHistory.HistoryEvent Deserialize(byte[] bytes)
        {
            return ME.ECS.Serializer.Serializer.Unpack<ME.ECS.StatesHistory.HistoryEvent>(bytes);
        }

        public byte[] SerializeWorld(ME.ECS.World.WorldState data)
        {
            return ME.ECS.Serializer.Serializer.Pack(data);
        }

        public ME.ECS.World.WorldState DeserializeWorld(byte[] bytes)
        {
            return ME.ECS.Serializer.Serializer.Unpack<ME.ECS.World.WorldState>(bytes);
        }
    }
}