using Newtonsoft.Json;
using System;
using System.Collections;

namespace SmartHome.Model
{
    public class Roles
    {
        [JsonProperty("roles")]
        public Array roles { get; set; }
    }
}
