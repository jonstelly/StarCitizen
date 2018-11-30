using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using StarCitizen.Locations;

namespace StarCitizen.StarMap.Internal
{
    public class ApiSolarSystem : NamedEntity
    {
        public SolarSystemStatus Status { get; set; }
        public DateTime TimeModified { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }

        public float AggregatedSize { get; set; }
        public float AggregatedPopulation { get; set; }
        public float AggregatedEconomy { get; set; }
        public float AggregatedDanger { get; set; }
        public JObject Thumbnail { get; set; }

        public Uri InfoUrl { get; set; }

        public string Description { get; set; }
    }
}