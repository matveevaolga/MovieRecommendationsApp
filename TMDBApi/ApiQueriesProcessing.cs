﻿using System;
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

        async public static Task<PageWithMovies> GetPageWithMovies(int page)
        {
            string query = string.Format("https://api.themoviedb.org/3/discover/movie?language=en&page={0}&sort_by=popularity.desc&api_key={1}",
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
    }
}
