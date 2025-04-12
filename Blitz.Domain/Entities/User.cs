using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blitz.Domain.Entities
{
    [Table("User")]
    public class User : IdentityUser
    {
        public User()
        {

        }

        public User(string email, string username)
        {
            UserName = username;
            Email = email;
        }

        public User(string email, string username, string phoneNumber)
        {
            Email = email;
            UserName = username;
            PhoneNumber = phoneNumber;
        }
        public bool IsAdmin { get; set; }
    }
}