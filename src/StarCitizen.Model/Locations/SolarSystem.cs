using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace StarCitizen.Locations
{
    public class SolarSystem : NamedEntity
    {
        public SolarSystemStatus Status { get; set; }
        public DateTime TimeModified { get; set; }
        public SolarSystemType Type { get; set; }
        public Vector3 Position { get; set; }

        public float Size { get; set; }
        public float Population { get; set; }
        public float Economy { get; set; }
        public float Danger { get; set; }
        public ThumbnailsInfo Thumbnail { get; set; }

        public Uri InfoUrl { get; set; }

        public string Description { get; set; }
    }
}