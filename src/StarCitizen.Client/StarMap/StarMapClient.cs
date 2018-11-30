using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StarCitizen.StarMap.Internal;
using StarCitizen.StarMap.Mappings;

namespace StarCitizen.StarMap
{
    public class StarMapInfo
    {
        public List<SolarSystem> Systems { get; set; }
    }

    public class StarMapClient : IDisposable
    {
        private readonly HttpClient _client;
        private readonly bool _disposeClient;
        private static readonly IMapper Mapper;

        static StarMapClient()
        {
            var config = new MapperConfiguration(mc =>
            {
                mc.AddProfile<ApiProfile>();
            });
            Mapper = config.CreateMapper();
        }

        public static readonly Uri BaseUrl = new Uri("https://robertsspaceindustries.com/api/starmap/");

        public StarMapClient(HttpClient client = null)
        {
            _client = client;
            if (_client == null)
            {
                _disposeClient = true;
                _client = new HttpClient();
            }
        }
        
        public async Task<StarMapInfo> GetStarMapAsync()
        {
            using (var resp = await _client.PostAsync(new Uri(BaseUrl, "bootup"), null))
            {
                resp.EnsureSuccessStatusCode();
                using (var s = await resp.Content.ReadAsStreamAsync())
                using (var txt = new StreamReader(s))
                using (var rd = new JsonTextReader(txt))
                {
                    var result = ApiSettings.Json.Deserialize<ApiResponse<ApiStarMapData>>(rd);
                    if(!result.Success)
                        throw new InvalidOperationException($"Failure getting star map: {result.Code} - {result.Message}");
                    return new StarMapInfo
                    {
                        Systems = result.Data.Systems.ResultSet.Select(sys => Mapper.Map<SolarSystem>(sys)).ToList()
                    };
                }
            }
        }

        public void Dispose()
        {
            if(_disposeClient)
                _client?.Dispose();
        }
    }
}
