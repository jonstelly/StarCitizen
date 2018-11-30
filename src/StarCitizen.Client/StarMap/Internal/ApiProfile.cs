using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using AutoMapper;
using Newtonsoft.Json.Linq;
using StarCitizen.Locations;

namespace StarCitizen.StarMap.Internal
{
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<ApiSolarSystem, SolarSystem>()
                .ForMember(ss => ss.Type,
                    m => m.MapFrom((api, ss) =>
                        api.Type == "SINGLE_STAR" ? SolarSystemType.Single : SolarSystemType.Binary))
                .ForMember(ss => ss.Thumbnail, m => m.MapFrom((api, ss) =>
                {
                    if (api.Thumbnail == null || api.Thumbnail["images"] is JArray)
                        return null;
                    return api.Thumbnail.ToObject<ThumbnailsInfo>(ApiSettings.Json);
                }))
                .ForMember(ss => ss.Position,
                    m => m.MapFrom((api, ss) => new Vector3(api.PositionX, api.PositionY, api.PositionZ)))
                .ForMember(ss => ss.Danger, m => m.MapFrom(api => api.AggregatedDanger))
                .ForMember(ss => ss.Economy, m => m.MapFrom(api => api.AggregatedEconomy))
                .ForMember(ss => ss.Population, m => m.MapFrom(api => api.AggregatedPopulation))
                .ForMember(ss => ss.Size, m => m.MapFrom(api => api.AggregatedSize))
                .ReverseMap();
        }
    }
}