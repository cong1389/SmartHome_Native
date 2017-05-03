using Newtonsoft.Json;

namespace SmartHome.Model
{
    public class Devices
    {
        [JsonProperty("deviceId")]
        public string deviceId { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("connection_code")]
        public string connection_code { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }
    }
}
