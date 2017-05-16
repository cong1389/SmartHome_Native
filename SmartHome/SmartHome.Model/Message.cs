using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartHome.Model
{
    public class Message
    {
        [JsonProperty("Success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string message { get; set; }
    }
}
