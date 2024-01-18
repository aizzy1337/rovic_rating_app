using Newtonsoft.Json;
using rovic_rating_app.Models.MovieAPI;

namespace rovic_rating_app.Models.SpotifyAPI
{
    public class SpotifyAPIResponse
    {
        [JsonProperty("albums")]
        public AlbumsAPIResponse? albums { get; set; }
    }

    public class AlbumsAPIResponse
    {
        [JsonProperty("items")]
        public List<ItemsAPIResponse>? items { get; set; }
    }

    public class ItemsAPIResponse
    {
        [JsonProperty("name")]
        public string? name { get; set; }
        [JsonProperty("release_date")]
        public string? release_date { get; set; }
        [JsonProperty("artists")]
        public List<ArtistsAPIResponse>? artists { get; set; }
        [JsonProperty("images")]
        public List<ImagesAPIResponse>? images { get; set; }
    }

    public class ArtistsAPIResponse
    {
        [JsonProperty("name")]
        public string? name { get; set; }
    }

    public class ImagesAPIResponse
    {
        [JsonProperty("url")]
        public string? url { get; set; }
    }
}
