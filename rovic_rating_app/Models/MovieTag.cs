namespace rovic_rating_app.Models
{
    public class MovieTag
    {
        public Movie? Movie { get; set; }
        public int MovieId { get; set; }
        public Tag? Tag { get; set; }
        public int TagId { get; set; }
    }
}
