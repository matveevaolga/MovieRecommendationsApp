using System.Windows.Media.Imaging;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Resources;
using Google.Protobuf.WellKnownTypes;


namespace TMDBApi
{
    public class Movie
    {
        readonly static ResourceManager resourceManager = new ResourceManager("TMDBApi.Properties.Resources",
            typeof(Movie).Assembly);
        public enum Genres
        {
            Action = 28,
            Adventure = 12,
            Animation = 16,
            Comedy = 35,
            Crime = 80,
            Documentary = 99,
            Drama = 18,
            Family = 10751,
            Fantasy = 14,
            History = 36,
            Horror = 27,
            Music = 10402,
            Mystery = 9648,
            Romance = 10749,
            ScienceFiction = 878,
            TVMovie = 10770,
            Thriller = 53,
            War = 10752,
            Western = 37,
        }
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
        public Uri GetPosterUri()
        {
            Uri uri = new Uri(string.Format("https://image.tmdb.org/t/p/w500{0}?" +
                "api_key={1}",
                PosterPath, resourceManager.GetString("ApiKey")));
            return uri;
            //var client = new HttpClient();
            //Stream body;
            //BitmapImage image = new BitmapImage();
            //var request = ApiQueriesProcessing.FormRequest(query);
            //using (var response = await client.SendAsync(request).ConfigureAwait(false))
            //{
            //    response.EnsureSuccessStatusCode();
            //    body = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            //    image.StreamSource = body;
            //}
            //return body;
        }
        public static string GetGenreById(int genreId)
        {
            try { var genre = (Genres)genreId; return genre.ToString(); }
            catch (KeyNotFoundException) { return "Unknown"; }
        }
    }
}
