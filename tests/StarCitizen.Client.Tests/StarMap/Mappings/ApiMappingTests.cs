using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using AutoMapper;
using Newtonsoft.Json;
using StarCitizen.StarMap.Internal;
using Xunit;
using Xunit.Abstractions;

namespace StarCitizen.StarMap.Mappings
{
    public class ApiMappingTests
    {
        private static readonly string StantonJson = @"
{
  ""id"": ""314"",
  ""status"": ""P"",
  ""time_modified"": ""2018-01-27 01:36:42"",
  ""type"": ""SINGLE_STAR"",
  ""name"": ""Stanton"",
  ""code"": ""STANTON"",
  ""position_x"": ""49.53471800"",
  ""position_y"": ""-2.63396450"",
  ""position_z"": ""16.47529200"",
  ""description"": ""While the UEE still controls the rights to the system overall, the four planets themselves were sold by the government to four megacorporations making them the only privately-owned worlds in the Empire. Though subject to the UEE's Common Laws and standard penal code, the UEE does not police the region. Instead, private planetary security teams enforce the local law."",
  ""info_url"": null,
  ""affiliation"": [
    {
      ""id"": ""1"",
      ""name"": ""UEE"",
      ""code"": ""uee"",
      ""color"": ""#48bbd4"",
      ""membership.id"": ""741""
    }
  ],
  ""aggregated_size"": ""4.85000000"",
  ""aggregated_population"": 10,
  ""aggregated_economy"": 10,
  ""aggregated_danger"": 10,
  ""thumbnail"": {
    ""slug"": ""anxi4tr0ija81"",
    ""source"": ""https://robertsspaceindustries.com/media/anxi4tr0ija81r/source/JStanton-Arccorp.jpg"",
    ""images"": {
      ""post"": ""https://robertsspaceindustries.com/media/anxi4tr0ija81r/post/JStanton-Arccorp.jpg"",
      ""product_thumb_large"": ""https://robertsspaceindustries.com/media/anxi4tr0ija81r/product_thumb_large/JStanton-Arccorp.jpg"",
      ""subscribers_vault_thumbnail"": ""https://robertsspaceindustries.com/media/anxi4tr0ija81r/subscribers_vault_thumbnail/JStanton-Arccorp.jpg""
    }
  }
}";
        private readonly ITestOutputHelper _output;
        private readonly IMapper _mapper;
        public ApiMappingTests(ITestOutputHelper output)
        {
            _output = output;
            var config = new MapperConfiguration(mc =>
            {
                mc.AddProfile<ApiProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void ConvertStarMapResponse()
        {
            var json = Encoding.UTF8.GetString(Json.ApiResponses.StarMapBootstrap);
            var result = JsonConvert.DeserializeObject<ApiResponse<ApiStarMapData>>(json, ApiSettings.JsonSettings);
            Assert.NotNull(result);
            var stanton = result.Data.Systems.ResultSet[0];
            Assert.NotNull(stanton);
            Assert.Equal("Stanton", stanton.Name);
            Assert.NotEqual(0f, stanton.PositionX);
            Assert.NotEqual(0f, stanton.PositionY);
            Assert.NotEqual(0f, stanton.PositionZ);
            Assert.NotEqual(0f, stanton.AggregatedDanger);
            Assert.NotEqual(0f, stanton.AggregatedEconomy);
            Assert.NotEqual(0f, stanton.AggregatedPopulation);
            Assert.NotEqual(0f, stanton.AggregatedSize);
        }

        [Fact]
        public void ConvertSolarSystem()
        {
            //var json = Encoding.UTF8.GetString(Json.ApiResponses.StarMapBootstrap);
            //var response = JsonConvert.DeserializeObject<JObject>(json, ApiSettings.JsonSettings);
            var api = JsonConvert.DeserializeObject<ApiSolarSystem>(StantonJson, ApiSettings.JsonSettings);
            var ss = _mapper.Map<SolarSystem>(api);
            Assert.NotNull(ss);
            Assert.NotEqual(ss.Position, Vector3.Zero);
            Assert.NotEqual(0f, ss.Danger);
            Assert.NotEqual(0f, ss.Economy);
            Assert.NotEqual(0f, ss.Population);
            Assert.NotEqual(0f, ss.Size);
        }
    }
}