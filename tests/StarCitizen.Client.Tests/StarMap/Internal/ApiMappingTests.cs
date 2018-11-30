using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using AutoMapper;
using Newtonsoft.Json;
using StarCitizen.Json;
using StarCitizen.Locations;
using Xunit;
using Xunit.Abstractions;

namespace StarCitizen.StarMap.Internal
{
    public class ApiMappingTests
    {
        public ApiMappingTests(ITestOutputHelper output)
        {
            _output = output;
            var config = new MapperConfiguration(mc => { mc.AddProfile<ApiProfile>(); });
            _mapper = config.CreateMapper();
        }

        private readonly ITestOutputHelper _output;
        private readonly IMapper _mapper;

        [Fact]
        public void ConvertApiStarMapInfo()
        {
            var json = Encoding.UTF8.GetString(ApiResponses.StarMapBootstrap);
            var apiInfo = JsonConvert.DeserializeObject<ApiResponse<ApiStarMapInfo>>(json, ApiSettings.JsonSettings);
            var info = _mapper.Map<StarMapInfo>(apiInfo.Data);
            Assert.NotNull(info);
            Assert.NotEmpty(info.Affiliations);
            Assert.NotEmpty(info.Species);
            Assert.NotEmpty(info.Systems);
        }

        [Fact]
        public void ConvertSolarSystem()
        {
            var api = JsonConvert.DeserializeObject<ApiSolarSystem>(Json.ApiResponses.StantonJson, ApiSettings.JsonSettings);
            var ss = _mapper.Map<SolarSystem>(api);
            Assert.NotNull(ss);
            Assert.NotEqual(ss.Position, Vector3.Zero);
            Assert.NotEqual(0f, ss.Danger);
            Assert.NotEqual(0f, ss.Economy);
            Assert.NotEqual(0f, ss.Population);
            Assert.NotEqual(0f, ss.Size);
        }

        [Fact]
        public void ConvertStarMapResponse()
        {
            var json = Encoding.UTF8.GetString(ApiResponses.StarMapBootstrap);
            var result = JsonConvert.DeserializeObject<ApiResponse<ApiStarMapInfo>>(json, ApiSettings.JsonSettings);
            Assert.NotNull(result);
            Assert.NotEmpty(result.Data.Affiliations.ResultSet);
            Assert.NotEmpty(result.Data.Species.ResultSet);
            Assert.NotEmpty(result.Data.Systems.ResultSet);

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
    }
}