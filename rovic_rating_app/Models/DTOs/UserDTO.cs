using System.ComponentModel.DataAnnotations;

namespace rovic_rating_app.Models.DTOs
{
    public class UserRegistrationDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }

    public class UserRegistrationResponseDTO : AuthResult
    {

    }

    public class UserLoginDTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }

    public class UserLoginResponseDTO : AuthResult
    {

    }
}
