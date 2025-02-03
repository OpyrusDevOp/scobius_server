namespace Scobius.DTOs;

public record FriendshipCreationDto
{
    public string SenderId { get; set; } = null!;
    public string ReceiverId { get; set; } = null!;
}

public record FriendshipDto
{
    public string UserAId { get; set; } = null!;
    public string UserBId { get; set; } = null!;
    public DateTime CreateAt { get; set; }
}


public record FriendshipRequestDto
{
    public Guid Id { get; set; }
    public string SenderId { get; set; } = null!;
    public string SenderName { get; set; } = null!;
    public string? SenderProfilePicture { get; set; }
    public FriendshipRequestStatus Status { get; set; } = FriendshipRequestStatus.Pending;
}
