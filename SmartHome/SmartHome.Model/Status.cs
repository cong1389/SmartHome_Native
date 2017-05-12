using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartHome.Model
{
    public class Status
    {
        [JsonProperty("Success")]
        public bool Success { get; set; }

        [JsonProperty("data")]
        public string data { get; set; }

        //[JsonProperty("error")]
        //public string error { get; set; }
    }
}
