using Newtonsoft.Json;

namespace SmartHome.Model
{
    public class ProductType
    {       

        [JsonProperty("productTypeId")]
        public string productTypeId { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("isSmart")]
        public bool isSmart { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }
    }
}
