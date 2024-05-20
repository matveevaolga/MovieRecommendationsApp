using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Resources;
using System.Windows.Controls;


namespace TMDBApi
{
    public static class ApiQueriesProcessing
    {
        static readonly JsonSerializerOptions options =
        new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static Dictionary<string, string> SortingParameters { get; } 
            = new Dictionary<string, string>()
        {
            { "Дата выхода", "primary_release_date" },
            { "Название", "title" },
            { "Выручка", "revenue" },
            { "Популярность", "popularity" },
            { "Рейтинг", "vote_average" }
        };

        public static Dictionary<string, string> SortingOrder { get; }
            = new Dictionary<string, string>()
        {
            { "Убывания", ".desc" },
            { "Возрастания", ".asc" }
        };

        readonly static ResourceManager resourceManager = new ResourceManager
            ("TMDBApi.Properties.Resources",
            typeof(ApiQueriesProcessing).Assembly);

        public static HttpRequestMessage FormRequest(string query)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(query),
                Headers =
                {
                    { "accept", "application/json" },
                },
            };
            return request;
        }

        async public static Task<PageWithMovies> GetPageWithMovies(int page, string parameters = "&sort_by=popularity.desc")
        {
            string query = string.Format("https://api.themoviedb.org/3/discover/movie?language=en&page={0}{1}&api_key={2}",
                page, parameters, resourceManager.GetString("ApiKey"));
            var client = new HttpClient();
            var request = FormRequest(query);
            PageWithMovies? pageWithMovies;
            using (var response = await client.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                pageWithMovies = JsonSerializer.Deserialize<PageWithMovies>(body, options);
                if (pageWithMovies == null ||
                    pageWithMovies.Results == null ||
                    pageWithMovies.Results.Count == 0)
                    throw new IndexOutOfRangeException();
            }
            return pageWithMovies;
        }

        async public static Task<MovieDetails> GetMovieDetails(int movieId)
        {
            string query = string.Format("https://api.themoviedb.org/3/movie/{0}?api_key={1}",
                movieId, resourceManager.GetString("ApiKey"));
            var client = new HttpClient();
            var request = FormRequest(query);
            MovieDetails movieDetails;
            using (var response = await client.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                movieDetails = JsonSerializer.Deserialize<MovieDetails>(body, options);
            }
            return movieDetails;
        }

        async public static Task<Movie> GetMovie(int movieId)
        {
            string query = string.Format("https://api.themoviedb.org/3/movie/{0}?api_key={1}",
                movieId, resourceManager.GetString("ApiKey"));
            var client = new HttpClient();
            var request = FormRequest(query);
            Movie movie;
            using (var response = await client.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                movie = JsonSerializer.Deserialize<Movie>(body, options);
            }
            return movie;
        }
    }
}
