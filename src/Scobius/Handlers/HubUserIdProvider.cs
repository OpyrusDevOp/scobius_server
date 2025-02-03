using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace Scobius.Handlers;

public class HubUserIdProvider : IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        return connection.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }
}
