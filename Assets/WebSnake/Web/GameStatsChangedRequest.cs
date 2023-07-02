using System;
using Newtonsoft.Json;

namespace WebSnake.Web
{
    [Serializable]
    public class GameStatsChangedRequest
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

        public GameStatsChangedRequest(int applesCount, int snakeLength, int gameId)
        {
            Payload = new Data
            {
                AppleCount = applesCount,
                SnakeLength = snakeLength,
                GameId = gameId
            };
        }
    }
}