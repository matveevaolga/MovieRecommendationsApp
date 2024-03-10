using System.Windows.Media.Imaging;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Resources;


namespace TMDBApi
{
    public class Movie
    {
        readonly static ResourceManager resourceManager = new ResourceManager("MovieRecommendationsApp.Properties.Resources",
            typeof(Movie).Assembly);
        public int Id { get; init; }
        [JsonPropertyName("genre_ids")]
        public List<int>? GenreIds { get; init; }
        public string? Title { get; init; }
        public string? Overview { get; init; }
        [JsonPropertyName("release_date")]
        public string? ReleaseDate { get; init; }
        [JsonPropertyName("poster_path")]
        public string? PosterPath { get; init; }
        public double Popularity { get; init; }
        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; init; }
        async public Task<BitmapImage> GetPoster()
        {
            string query = string.Format("https://image.tmdb.org/t/p/w500{0}?api_key={1}",
                PosterPath, resourceManager.GetString("ApiKey"));
            var client = new HttpClient();
            BitmapImage image = new BitmapImage();
            var request = ApiQueriesProcessing.FormRequest(query);
            using (var response = await client.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                image.StreamSource = body;
            }
            return image;
        }
    }
}
