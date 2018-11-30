using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarCitizen.StarMap.Internal
{
    public class ApiStarMapData
    {
        public ApiPage<Race> Species { get; set; }
        public ApiPage<Faction> Affiliations { get; set; }
        public ApiPage<ApiSolarSystem> Systems { get; set; }
    }
}