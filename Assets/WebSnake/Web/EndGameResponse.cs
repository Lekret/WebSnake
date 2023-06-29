using System;
using Newtonsoft.Json;

namespace WebSnake.Web
{
    [Serializable]
    public class EndGameResponse
    {
        [Serializable]
        public class Data
        {
            [JsonProperty("id")] public int Id;
            [JsonProperty("startAt")] public string StartAt;
            [JsonProperty("finishAt")] public string FinishAt;
            [JsonProperty("clientAddress")] public string ClientAddress;
            [JsonProperty("collectedApples")] public int CollectedApples;
            [JsonProperty("snakeLength")] public int SnakeLength;
            [JsonProperty("created_at")] public DateTime CreatedAt;
            [JsonProperty("updated_at")] public DateTime UpdatedAt;
        }

        [JsonProperty("type")] public string Type;
        [JsonProperty("payload")] public Data Payload;
    }
}