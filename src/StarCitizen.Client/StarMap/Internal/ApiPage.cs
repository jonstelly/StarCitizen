using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace StarCitizen.StarMap.Internal
{
    public class ApiPage<T>
    {
        public int Page { get; set; }
        public int Offset { get; set; }

        [JsonProperty("resultset")] public List<T> ResultSet { get; set; }
    }
}