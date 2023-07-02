using System;
using Newtonsoft.Json;

namespace WebSnake.Web
{
    [Serializable]
    public class EndGameRequest
    {
        [Serializable]
        public class Data
        {
            [JsonProperty("game_id")] public int GameId;
        }

        [JsonProperty("type")] public readonly string Type = "end-game";
        [JsonProperty("payload")] public readonly Data Payload;

        public EndGameRequest(int gameId)
        {
            Payload = new Data
            {
                GameId = gameId
            };
        }
    }
}