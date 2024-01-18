using Newtonsoft.Json;
using RestSharp;
using rovic_rating_app.Models.MovieAPI;
using rovic_rating_app.Models;
using rovic_rating_app.Models.SpotifyAPI;
using System.Web;

namespace rovic_rating_app.Helpers
{
    public class SearchAlbumHelper
    {
        private readonly IConfiguration configuration;

        public SearchAlbumHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<Album>> Search(string text)
        {
            var options = new RestClientOptions("https://api.spotify.com/v1/search?q=" + HttpUtility.UrlEncode(text) + "&type=album&limit=5");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("Authorization", configuration.GetValue<string>("ApiKeys:SpotifyAPI").ToString());
            var response = await client.GetAsync(request);

            SpotifyAPIResponse? spotifyAPIResponse = JsonConvert.DeserializeObject<SpotifyAPIResponse>(response.Content);

            List<Album> results = spotifyAPIResponse.albums.items.Select(a => new Album()
            {
                Title = a.name,
                Artist = a.artists[0].name,
                ProductionYear = int.Parse(a.release_date.Substring(0, 4)),
                Cover = a.images[0].url
            }).ToList();

            return results;
        }
    }
}
