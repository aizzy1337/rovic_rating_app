namespace rovic_rating_app.Models
{
    public class AlbumTag
    {
        public Album? Album { get; set; }
        public int AlbumId { get; set; }
        public Tag? Tag { get; set; }
        public int TagId { get; set; }
    }
}
