using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using StarCitizen.Locations;
using StarCitizen.StarMap.Internal;

namespace StarCitizen.StarMap
{
    public class StarMapClient : IDisposable
    {
        private static readonly IMapper Mapper;

        public static readonly Uri BaseUrl = new Uri("https://robertsspaceindustries.com/api/starmap/");

        static StarMapClient()
        {
            var config = new MapperConfiguration(mc => { mc.AddProfile<ApiProfile>(); });
            Mapper = config.CreateMapper();
        }

        public StarMapClient(HttpClient client = null)
        {
            _client = client;
            if (_client == null)
            {
                _disposeClient = true;
                _client = new HttpClient();
            }
        }

        private readonly HttpClient _client;
        private readonly bool _disposeClient;

        public void Dispose()
        {
            if (_disposeClient)
                _client?.Dispose();
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
                    if (!result.Success)
                        throw new InvalidOperationException(
                            $"Failure getting star map: {result.Code} - {result.Message}");
                    return new StarMapInfo
                    {
                        Systems = result.Data.Systems.ResultSet.Select(sys => Mapper.Map<SolarSystem>(sys)).ToList(),
                        Affiliations = result.Data.Affiliations.ResultSet,
                        Species = result.Data.Species.ResultSet
                    };
                }
            }
        }
    }
}