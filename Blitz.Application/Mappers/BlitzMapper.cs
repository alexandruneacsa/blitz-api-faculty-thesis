using Blitz.Application.Dtos;
using Blitz.Domain.Entities;

namespace Blitz.Application.Mappers
{
    public class BlitzMapper
    {
        public static SingleUser MapSourceToDestination(User user) =>
            new()
            {
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                PhoneNumber = user.PhoneNumber,
                UserId = user.Id,
                Username = user.UserName,
            };
    }
}