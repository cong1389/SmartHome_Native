﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartHome.Model
{
    public class House
    {
        [JsonProperty("houseId")]
        public string houseId { get; set; }

        [JsonProperty("users")]
        public List<User> users { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("code")]
        public string code { get; set; }

        [JsonProperty("address")]
        public string address { get; set; }

        [JsonProperty("active")]
        public bool active { get; set; }

        [JsonProperty("rooms")]
        public List<Room> rooms { get; set; }

       


    }
}
