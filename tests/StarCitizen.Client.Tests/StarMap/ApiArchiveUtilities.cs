using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StarCitizen.Json;
using Xunit;
using Xunit.Abstractions;

namespace StarCitizen.StarMap
{
    public class ApiArchiveUtilities
    {
        public ApiArchiveUtilities(ITestOutputHelper output)
        {
            _output = output;
        }

        private static readonly Uri BaseUrl = new Uri("https://robertsspaceindustries.com/api/starmap/");
        private readonly ITestOutputHelper _output;

        [Fact]
        public async Task ValidateStarMapJson()
        {
            using (var client = new HttpClient())
            {
                using (var resp = await client.PostAsync(new Uri(BaseUrl, "bootup"), null))
                {
                    resp.EnsureSuccessStatusCode();
                    var actual = JsonConvert.SerializeObject(
                        JsonConvert.DeserializeObject(await resp.Content.ReadAsStringAsync()), Formatting.Indented);
                    var expected = JsonConvert.SerializeObject(
                        JsonConvert.DeserializeObject(Encoding.UTF8.GetString(ApiResponses.StarMapBootstrap)),
                        Formatting.Indented);
                    if (actual != expected)
                    {
                        _output.WriteLine("JSON:\r\n{0}", actual);
                        throw new InvalidOperationException("Change detected in star map JSON");
                    }
                }
            }
        }
    }
}