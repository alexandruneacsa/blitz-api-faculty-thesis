using System.ComponentModel.DataAnnotations;

namespace Blitz.Application.Dtos
{
    public class UserLoginModel
    {
        [Required]
        [MinLength(5)]
        public string Email { get; set; }
        [Required]
        [MinLength(5)]
        public string Password { get; set; }
    }
}
