using Newtonsoft.Json;

namespace SmartHome.Model
{
    public class Apps
    {
        [JsonProperty("appId")]
        public string appId { get; set; }
    }
}
