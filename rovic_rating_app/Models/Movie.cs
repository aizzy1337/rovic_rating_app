using System.ComponentModel.DataAnnotations;

namespace rovic_rating_app.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ProductionYear { get; set; }
        public string Poster { get; set; }
        public int Rate { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public ICollection<MovieTag>? MovieTags { get; set; }
    }
}
