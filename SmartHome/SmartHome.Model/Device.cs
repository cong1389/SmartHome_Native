using Newtonsoft.Json;

namespace SmartHome.Model
{
    public class Devices
    {
        [JsonProperty("deviceId")]
        public string deviceId { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("code")]
        public string code { get; set; }

        [JsonProperty("paired")]
        public bool paired { get; set; }

        [JsonProperty("status")]
        public bool status { get; set; }
        
        [JsonProperty("productTypeId")]
        public string productTypeId { get; set; }

        [JsonProperty("url")]
        public string url { get; set; }

        [JsonProperty("macAdd")]
        public string macAdd { get; set; }

        [JsonProperty("port")]
        public string port { get; set; }
    }
}
