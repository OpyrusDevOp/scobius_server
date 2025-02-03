namespace Scobius.DTOs;

public record SignupDto
{
    public required string Username { get; set; } = null!;
    public required string Email { get; set; } = null!;
    public required string Password { get; set; } = null!;
}

public record UserDto
{
    public required string Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; }
    public string? ProfilePicture { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime LastSeen { get; set; }
}


public record UserQueryDto
{
    public required string Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? ProfilePicture { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime LastSeen { get; set; }
    public bool IsFriend { get; set; }
    public bool PendingFriendRequest { get; set; }
}
