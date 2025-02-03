using Microsoft.EntityFrameworkCore;
using Scobius.DTOs;
using Scobius.Entities;
using Scobius.Handlers;

namespace Scobius.Services;

public class ChatService(ScobiusTest_Context context)
{

    public async Task<ChatDto[]> GetChats(string userId)
    {
        var chats = context.Chats
          .Where(c => c.UserAId == userId || c.UserBId == userId)
          .Include(c => c.UserA)
          .Include(c => c.UserB)
          .ToList();

        var chatDtos = chats.Select(c =>
          {
              var messages = GetLastMessage(c.Id);
              var lastMessage = messages.Length > 0 ? messages.First() : null;
              return DtoHandler.GetChatDto(userId, c, lastMessage);
          });

        return chatDtos.ToArray();
    }


    public async Task<MessageDto> SaveMessage(SendMessageDto messageDto, string senderId)
    {
        var message = new Message()
        {
            SenderId = senderId,
            ChatId = messageDto.ChatId,
            MessageContent = messageDto.Message,
            Seen = false,
            MessageRepliedTo = messageDto.RepliedTo,
            SendAt = DateTime.UtcNow
        };

        var entry = await context.Messages.AddAsync(message);
        await context.SaveChangesAsync();

        return DtoHandler.GetMessageDto(entry.Entity);
    }

    public MessageDto[] GetChatMessages(Guid chatId)
    {

        var messages = context.Messages.
          Where(m => m.ChatId == chatId).Take(10);

        if (!messages.Any()) return [];
        var chatMessagesDtos = messages.Select(DtoHandler.GetMessageDto).ToArray();

        return chatMessagesDtos;
    }

    private Message[] GetLastMessage(Guid chatId)
    {
        return context.Messages
          .OrderByDescending(m => m.SendAt)
          .Take(10)
          .ToArray();
    }
}
