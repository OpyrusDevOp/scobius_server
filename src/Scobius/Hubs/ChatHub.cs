using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Scobius.DTOs;
using Scobius.Services;

namespace Scobius.Hubs;

[Authorize]
public class ChatHub(ChatService chatService) : Hub
{

    public async Task JoinChat(string chatId)
        => await Groups.AddToGroupAsync(Context.ConnectionId, chatId);

    public async Task SendMessage(SendMessageDto messageDto)
    {
        string id = Context.UserIdentifier!;
        try
        {
            MessageDto message = await chatService.SaveMessage(messageDto, id);

            await Clients.Group(message.ChatId.ToString()).SendAsync("ReceiveMessage", message);
        }
        catch (Exception)
        {
            await Clients.Caller.SendAsync("MessageSendError", messageDto);
        }
    }

    public async Task GetMessages(Guid chatId)
    {
        var messages = chatService.GetChatMessages(chatId);

        await Clients.Caller.SendAsync("GetChatMessages", messages);
    }

}
