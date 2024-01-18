using Newtonsoft.Json;

namespace rovic_rating_app.Models.MovieAPI
{
    public class MovieAPIResponse
    {
        [JsonProperty("results")]
        public List<ResultAPIResponse>? results { get; set; }
    }

    public class ResultAPIResponse
    {
        [JsonProperty("original_title")]
        public string? original_title { get; set; }
        [JsonProperty("overview")]
        public string? overview { get; set; }
        [JsonProperty("poster_path")]
        public string? poster_path { get; set; }
        [JsonProperty("release_date")]
        public string? release_date { get; set; }
    }
}
