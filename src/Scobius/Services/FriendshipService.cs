using Microsoft.EntityFrameworkCore;
using Scobius.DTOs;
using Scobius.Entities;
using Scobius.Handlers;

namespace Scobius.Services;

public class FrienshipService(ScobiusTest_Context context)
{

    public async Task<FriendshipRequestDto> RequestFrienship(FriendshipCreationDto info)
    {
        bool userAExist = context.Users.Any(u => u.Id == info.SenderId);
        bool userBExist = context.Users.Any(u => u.Id == info.ReceiverId);

        if (!userAExist || !userBExist)
            throw new FormatException();

        FriendshipRequest friendship = new()
        {
            SenderId = info.SenderId,
            ReceiverId = info.ReceiverId,
            CreateAt = DateTime.UtcNow
        };

        var entry = await context.FriendshipRequests.AddAsync(friendship);
        await context.SaveChangesAsync();
        context.Entry(entry.Entity).Reference(r => r.Sender).Load();
        return DtoHandler.GetFriendshipRequestDto(entry.Entity);
    }
    public FriendshipRequestDto[] GetMyRequests(string userId)
    {
        var requests = context.FriendshipRequests
          .Include(r => r.Sender)
          .Where(r => r.ReceiverId == userId && r.Status == FriendshipRequestStatus.Pending);

        var requestDto = requests.Select(DtoHandler.GetFriendshipRequestDto);

        return requestDto.ToArray();
    }

    public async Task<Chat?> AcceptRequest(Guid requestId)
    {

        if (!context.FriendshipRequests.Any(r => r.Id == requestId)) return null;
        var newStatus = FriendshipRequestStatus.Accepted;

        var transaction = context.Database.BeginTransaction();
        var request = await UpdateRequest(requestId, newStatus);

        var friendship = new Friendship()
        {
            UserAId = request.SenderId,
            UserBId = request.ReceiverId,
            CreateAt = DateTime.UtcNow
        };

        await context.Friendships.AddAsync(friendship);
        await context.SaveChangesAsync();

        var chat = new Chat()
        {
            UserAId = friendship.UserAId,
            UserBId = friendship.UserBId,
            CreateAt = DateTime.UtcNow
        };

        var entry = await context.Chats.AddAsync(chat);
        await context.SaveChangesAsync();

        await transaction.CommitAsync();

        context.Entry(entry.Entity).Reference(c => c.UserA);
        context.Entry(entry.Entity).Reference(c => c.UserB);
        return entry.Entity;
    }

    public async Task RejectRequest(Guid requestId)
      => await UpdateRequest(requestId, FriendshipRequestStatus.Rejected);

    async Task<FriendshipRequest> UpdateRequest(Guid requestId, FriendshipRequestStatus newStatus)
    {
        var request = await context.FriendshipRequests.FindAsync(requestId);

        request.Status = newStatus;

        var entry = context.FriendshipRequests.Update(request);

        await context.SaveChangesAsync();

        return entry.Entity;
    }

    public FriendshipDto[] GetFriendships(string userId)
    {
        IEnumerable<FriendshipDto> friendships = context.Friendships
          .Where(f => f.UserAId == userId || f.UserBId == userId)
          .Select(DtoHandler.GetFriendshipDto);

        return [.. friendships];
    }
}
