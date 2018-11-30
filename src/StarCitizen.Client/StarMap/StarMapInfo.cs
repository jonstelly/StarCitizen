using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StarCitizen.Locations;

namespace StarCitizen.StarMap
{
    public class StarMapInfo
    {
        public List<Race> Species { get; set; }
        public List<Faction> Affiliations { get; set; }
        public List<SolarSystem> Systems { get; set; }
    }
}