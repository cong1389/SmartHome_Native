using Newtonsoft.Json;

namespace SmartHome.Model
{
    public class Product
    {       

        [JsonProperty("productId")]
        public string productId { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("productTypeId")]
        public string productTypeId { get; set; }
    }
}
