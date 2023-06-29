using System;
using Newtonsoft.Json;

namespace WebSnake.Web
{
    [Serializable]
    public class CreateGameResponse
    {
        [Serializable]
        public class Data
        {
            [JsonProperty("clientAddress")] public string ClientAddress;
            [JsonProperty("startAt")] public string StartAt;
            [JsonProperty("finishAt")] public string FinishAt;
            [JsonProperty("id")] public int Id;
            [JsonProperty("collectedApples")] public int CollectedApples;
            [JsonProperty("snakeLength")] public int SnakeLength;
            [JsonProperty("created_at")] public DateTime CreatedAt;
            [JsonProperty("updated_at")] public DateTime UpdatedAt;
        }

        [JsonProperty("type")] public string Type;
        [JsonProperty("payload")] public Data Payload;
    }
}