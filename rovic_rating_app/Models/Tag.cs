using System.ComponentModel.DataAnnotations;

namespace rovic_rating_app.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMovieTag { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public ICollection<MovieTag>? MovieTags { get; set; }
        public ICollection<AlbumTag>? AlbumTags { get; set; }
    }
}
