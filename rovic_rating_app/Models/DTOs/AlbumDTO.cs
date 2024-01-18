using System.ComponentModel.DataAnnotations;

namespace rovic_rating_app.Models.DTOs
{
    public class AlbumGetDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public int ProductionYear { get; set; }
        public string Cover { get; set; }
        public int Rate { get; set; }
        public List<Tag> Tags { get; set; }
}

    public class AlbumUpdateDTO
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter value bigger then 1")]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Artist { get; set; }
        [Required]
        [Range(1000, 2050)]
        public int ProductionYear { get; set; }
        [Required]
        public string Cover { get; set; }
        [Required]
        [Range(1, 10)]
        public int Rate { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter value bigger then 1")]
        public int UserId { get; set; }
    }

    public class AlbumPostDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Artist { get; set; }
        [Required]
        [Range(1000, 2050)]
        public int ProductionYear { get; set; }
        [Required]
        public string Cover { get; set; }
        [Required]
        [Range(1, 10)]
        public int Rate { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter value bigger then 1")]
        public int UserId { get; set; }
    }
}
