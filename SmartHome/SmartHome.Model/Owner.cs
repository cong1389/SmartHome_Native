using Newtonsoft.Json;

namespace SmartHome.Model
{
    public class owner
    {
        //[JsonProperty("ownerId")]
        //public string ownerId { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("phone")]
        public string phone { get; set; }
    }
}
