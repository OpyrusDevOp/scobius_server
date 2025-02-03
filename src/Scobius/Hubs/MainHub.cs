using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Scobius.DTOs;
using Scobius.Services;

namespace Scobius.Hubs;

[Authorize]
public class MainHub(FrienshipService frienshipService, ChatService chatService) : Hub
{
    public async Task RequestFriendship(string friendId)
    {
        var userId = Context.UserIdentifier;

        var request = await frienshipService.RequestFrienship(
            new()
            {
                SenderId = userId,
                ReceiverId = friendId
            }
            );


        Clients.User(friendId).SendAsync("ReceiveFriendRequest", request);
    }

    public async Task GetChats()
    {
        string userId = Context.UserIdentifier!;

        ChatDto[] chats = await chatService.GetChats(userId);

        await Clients.Caller.SendAsync("ReceiveChats", chats);
    }

}
