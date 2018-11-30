using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Newtonsoft.Json;

namespace StarCitizen.StarMap
{
    public enum SolarSystemType
    {
        Single,
        Binary
    }

    //id, status, time_modified, type, name, code, position_x, position_y, position_z, description, info_url, affiliation, aggregated_size, aggregated_population, aggregated_economy, aggregated_danger, thumbnail
    public class SolarSystem : NamedEntity
    {
        public SolarSystemStatus Status { get; set; }
        public DateTime TimeModified { get; set; }
        public SolarSystemType Type { get; set; }
        public string Code { get; set; }
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
