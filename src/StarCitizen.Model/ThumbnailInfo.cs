using System;
using System.Collections.Generic;
using System.Text;

namespace StarCitizen
{
    public class ThumbnailsInfo
    {
        public string Slug { get; set; }
        public Uri Source { get; set; }
        public Dictionary<string, Uri> Images { get; set; }
    }
}
