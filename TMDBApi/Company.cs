using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TMDBApi
{
    public class Company
    {
        public long Id { get; init; }
        public string? Name { get; init; }
        [JsonPropertyName("origin_country")]
        public string? OriginCountry { get; init; }
    }
}
