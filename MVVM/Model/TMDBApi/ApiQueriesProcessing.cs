using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Resources;


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

        readonly static ResourceManager resourceManager = new ResourceManager("MovieRecommendationsApp.Properties.Resources",
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

        async public static Task<string> GetGenreById(int id)
        {
            string query = string.Format("https://api.themoviedb.org/3/genre/movie/list?language=ru&api_key={0}",
                resourceManager.GetString("ApiKey"));
            var client = new HttpClient();
            var request = FormRequest(query);
            string name;
            using (var response = await client.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                Dictionary<string, List<Genre>> genresDict =
                    JsonSerializer.Deserialize<Dictionary<string, List<Genre>>>(body, options);
                List<Genre> genres = genresDict["genres"];
                Genre genreInfo = genres.Find(x => x.Id == id);
                name = genreInfo.Name;
            }
            return name;
        }
        async public static Task<int> GetIdByGenre(string genre)
        {
            string query = string.Format("https://api.themoviedb.org/3/genre/movie/list?language=ru&api_key={0}",
                resourceManager.GetString("ApiKey"));
            var client = new HttpClient();
            var request = FormRequest(query);
            int id;
            using (var response = await client.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                Dictionary<string, List<Genre>> genresDict =
                    JsonSerializer.Deserialize<Dictionary<string, List<Genre>>>(body, options);
                List<Genre> genres = genresDict["genres"];
                Genre genreInfo = genres.Find(x => x.Name == genre);
                id = genreInfo.Id;
            }
            return id;
        }
        async public static Task<PageWithMovies> GetPageWithMovies(int page)
        {
            string query = string.Format("https://api.themoviedb.org/3/discover/movie?language=ru&page={0}&sort_by=popularity.desc&api_key={1}",
                page, resourceManager.GetString("ApiKey"));
            var client = new HttpClient();
            var request = FormRequest(query);
            PageWithMovies pageWithMovies;
            using (var response = await client.SendAsync(request).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                pageWithMovies = JsonSerializer.Deserialize<PageWithMovies>(body, options);
            }
            return pageWithMovies;
        }
    }
}
