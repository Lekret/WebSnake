using System;
using Newtonsoft.Json;

namespace WebSnake.Web
{
    [Serializable]
    public class AppleCollectedRequest
    {
        [Serializable]
        public class Data
        {
            [JsonProperty("appleCount")] public int AppleCount;
            [JsonProperty("snakeLength")] public int SnakeLength;
            [JsonProperty("game_id")] public int GameId;
        }

        [JsonProperty("type")] public readonly string Type = "collect-apple";
        [JsonProperty("payload")] public Data Payload;

        public AppleCollectedRequest()
        {
            Payload = new Data();
        }
    }
}