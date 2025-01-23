using Scobius.DTOs;
using Scobius.Entities;

namespace Scobius.Handlers;


public abstract class DtoHandler
{

    public static UserDto ToUserDto(User user)
      => new()
      {
          Id = user.Id,
          Username = user.Username,
          ProfilePicture = user.ProfilePicture,
          CreateAt = user.CreateAt,
          LastSeen = user.LastSeen
      };
}
