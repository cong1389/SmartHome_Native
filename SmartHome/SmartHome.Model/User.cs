using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartHome.Model
{
    public class User
    {
        [JsonProperty("userId")]
        public string userId { get; set; }        

        [JsonProperty("apps")]
        public List<Apps> apps { get; set; }

        [JsonProperty("houses")]
        public List<House> houses { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("scopes")]
        public List<Scopes> scopes { get; set; }        

        [JsonProperty("deviceId")]
        public string deviceId { get; set; }

        [JsonProperty("tenantId")]
        public string tenantId { get; set; }

        [JsonProperty("username")]
        public string username { get; set; }

        [JsonProperty("password")]
        public string password { get; set; }

        [JsonProperty("mobile")]
        public string mobile { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("address")]
        public string address { get; set; }

        [JsonProperty("active")]
        public bool active { get; set; }

        [JsonProperty("accessToken")]
        public string accessToken { get; set; }
    }
}
