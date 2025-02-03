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
          Email = user.Email,
          ProfilePicture = user.ProfilePicture,
          CreateAt = user.CreateAt,
          LastSeen = user.LastSeen
      };


    public static UserQueryDto ToUserQueryDto(User user, bool isFriend, bool hasPendingRequest)
      => new()
      {
          Id = user.Id,
          Username = user.Username,
          Email = user.Email,
          ProfilePicture = user.ProfilePicture,
          IsFriend = isFriend,
          PendingFriendRequest = hasPendingRequest,
          CreateAt = user.CreateAt,
          LastSeen = user.LastSeen
      };

    public static FriendshipRequestDto GetFriendshipRequestDto(FriendshipRequest request)
      => new()
      {
          Id = request.Id,
          SenderId = request.SenderId,
          SenderName = request.Sender.Username,
          SenderProfilePicture = request.Sender.ProfilePicture,
          Status = request.Status
      };

    public static FriendshipDto GetFriendshipDto(Friendship friendship)
      => new()
      {
          UserAId = friendship.UserAId,
          UserBId = friendship.UserBId,
          CreateAt = friendship.CreateAt
      };

    public static MessageDto GetMessageDto(Message message)
      => new()
      {
          Id = message.Id,
          ChatId = message.ChatId,
          SenderId = message.SenderId,
          JointMedia = null,
          Message = message.MessageContent,
          RepliedTo = message.MessageRepliedTo,
          Seen = message.Seen,
          SendAt = message.SendAt
      };

    public static ChatDto GetChatDto(string userId, Chat chat, Message? lastMessage)
    {
        bool isUserA = chat.UserAId == userId;

        return new()
        {
            Id = chat.Id,
            CreateAt = chat.CreateAt,
            InterlocutorId = isUserA ? chat.UserBId : chat.UserAId,
            InterlocutorName = isUserA ? chat.UserB.Username : chat.UserA.Username,
            InterlocutorPicture = isUserA ? chat.UserB.ProfilePicture : chat.UserA.ProfilePicture,
            LastMessage = GetLastMessageDto(lastMessage)
        };
    }

    private static LastMessageDto? GetLastMessageDto(Message? lastMessage)
    {
        if (lastMessage == null)
            return null;

        return new()
        {
            Message = lastMessage.MessageContent,
            LastMessageSendTime = lastMessage.SendAt
        };
    }
}
