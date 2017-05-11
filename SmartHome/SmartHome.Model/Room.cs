using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartHome.Model
{
    public class Room
    {
        [JsonProperty("roomId")]
        public string roomId { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("code")]
        public string code { get; set; }

        [JsonProperty("devices")]
        public List<Devices> devices { get; set; }
    }
}
