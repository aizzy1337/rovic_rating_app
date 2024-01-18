using MediatR;
using Newtonsoft.Json;
using NuGet.Protocol;
using RestSharp;
using rovic_rating_app.Models;
using rovic_rating_app.Models.MovieAPI;
using System.Text.Json;
using System.Web;

namespace rovic_rating_app.Helpers
{
    public class SearchMovieHelper
    {
        private readonly IConfiguration configuration;

        public SearchMovieHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<Movie>> Search(string text)
        {
            var options = new RestClientOptions("https://api.themoviedb.org/3/search/movie?query=" + HttpUtility.UrlEncode(text) + "&include_adult=false&language=en-US&page=1");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", configuration.GetValue<string>("ApiKeys:MovieAPI").ToString());
            var response = await client.GetAsync(request);

            MovieAPIResponse? movieAPIResponse = JsonConvert.DeserializeObject<MovieAPIResponse>(response.Content);

            List<Movie> results = movieAPIResponse.results.Take(5).Select(m => new Movie()
            {
                Title = m.original_title,
                Description = m.overview,
                ProductionYear = int.Parse(m.release_date.Length == 10 ? m.release_date.Substring(0, 4) : "0"),
                Poster = "https://image.tmdb.org/t/p/w500/" + m.poster_path
            }).ToList();

            return results;
        }
    }
}
