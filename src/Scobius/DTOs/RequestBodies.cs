namespace Scobius.DTOs;

public record UpdateFriendRquestBody
{

    public Guid requestId { get; set; }
    public FriendshipRequestStatus newStatus { get; set; }
}
