using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StarCitizen.StarMap.Internal;

namespace StarCitizen.StarMap.Internal
{
    public class ApiStarMapData
    {
        public ApiPage<ApiSolarSystem> Systems { get; set; }
    }
}