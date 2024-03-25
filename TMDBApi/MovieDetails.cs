using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TMDBApi
{
    public class MovieDetails
    {
        public bool Adult { get; init; }
        public int Revenue { get; init; }
        public int Budget { get; init; }
        public string? Homepage { get; init; }
        public int Runtime { get; init; }
        [JsonPropertyName("production_companies")]
        public List<Company>? ProductionCompanies {get; init;}
        public string? Status { get; init; }
        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; init; }
        [JsonPropertyName("vote_count")]
        public int VoteCount { get; init; }
    }
}
