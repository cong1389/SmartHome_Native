using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome.Model
{
    public class Hello
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Version")]
        public string Version { get; set; }
    }
}
