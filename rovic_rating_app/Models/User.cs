using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace rovic_rating_app.Models
{
    public class User : IdentityUser<int>
    {
        public ICollection<Tag>? Tags { get; set; }
        public ICollection<Movie>? Movies { get; set; }
        public ICollection<Album>? Albums { get; set; }
    }
}
