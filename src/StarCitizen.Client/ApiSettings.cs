using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace StarCitizen
{
    public static class ApiSettings
    {
        public static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy(false, true)
            },
            Converters = new JsonConverter[]
            {
                new StringEnumConverter()
            }
        };

        public static readonly JsonSerializer Json = JsonSerializer.Create(JsonSettings);
    }
}