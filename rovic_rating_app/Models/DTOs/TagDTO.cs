using System.ComponentModel.DataAnnotations;

namespace rovic_rating_app.Models.DTOs
{
    public class TagGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMovieTag { get; set; }
    }

    public class TagPostDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsMovieTag { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter value bigger then 1")]
        public int UserId { get; set; }
    }
}
