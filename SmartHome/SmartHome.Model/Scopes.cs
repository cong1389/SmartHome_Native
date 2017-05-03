using Newtonsoft.Json;

namespace SmartHome.Model
{
    public class Scopes
    {
        [JsonProperty("scopeId")]
        public string scopeId { get; set; }
    }
}
