using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartHome.Model
{
    public class Company
    {
        [JsonProperty("companyId")]
        public string companyId { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("products")]
        public List<Product> products { get; set; }
       
    }
}
