namespace Blitz.Application.Dtos
{
    public class UserDetailsModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public string JwtToken { get; set; }
        public bool IsAdmin { get; set; }
    }
}