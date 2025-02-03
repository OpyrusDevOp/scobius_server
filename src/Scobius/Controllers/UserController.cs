using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Scobius.DTOs;
using Scobius.Handlers;
using Scobius.Hubs;
using Scobius.Services;

namespace Scobius.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(UserService userService, FrienshipService friendshipService, IHubContext<MainHub> mainHub) : ControllerBase
{

    [HttpPost("[action]")]
    public async Task<ActionResult<UserDto>> Registration([FromBody] string uid)
    {
        try
        {
            if (await userService.Exists(uid))
            {
                return Ok();
            }

            var user = await userService.CompleteRegistration(uid);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [Authorize]
    [HttpGet("[action]")]
    public ActionResult<FriendshipRequestDto[]> GetMyFriendRequest()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var requests = friendshipService.GetMyRequests(userId);

        return requests;
    }

    [Authorize]
    [HttpGet("[action]")]
    public async Task<ActionResult<UserQueryDto[]>> SearchUser([FromQuery] string name)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        return await userService.SearchUsers(name, userId);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> UpdateFriendRequest([FromBody] UpdateFriendRquestBody body)
    {
        switch (body.newStatus)
        {
            case FriendshipRequestStatus.Accepted:
                var chat = await friendshipService.AcceptRequest(body.requestId);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                if (chat == null) return BadRequest();
                else mainHub.Clients.User(userId)
                        .SendAsync("ReceiveChats", DtoHandler.GetChatDto(userId, chat, null));
                break;
            case FriendshipRequestStatus.Rejected:
                await friendshipService.RejectRequest(body.requestId);
                break;
            default:
                return BadRequest();
        }

        return Ok();
    }
    [Authorize]
    [HttpPost("[action]")]
    public async Task<ActionResult> RequestFriendship([FromBody] string friendId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        await friendshipService.RequestFrienship(
            new()
            {
                SenderId = userId,
                ReceiverId = friendId
            }
            );


        return Ok();
    }
}
