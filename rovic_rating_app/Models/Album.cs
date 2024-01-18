using System.ComponentModel.DataAnnotations;

namespace rovic_rating_app.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public int ProductionYear { get; set; }
        public string Cover { get; set; }
        public int Rate { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public ICollection<AlbumTag>? AlbumTags { get; set; }
    }
}
