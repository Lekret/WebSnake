using System;
using Newtonsoft.Json;

namespace WebSnake.Web
{
    [Serializable]
    public class CreateGameRequest
    {
        [JsonProperty("type")] public readonly string Type = "create-game";
    }
}