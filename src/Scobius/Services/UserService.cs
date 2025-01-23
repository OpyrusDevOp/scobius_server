using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;
using Scobius.DTOs;
using Scobius.Entities;
using Scobius.Handlers;

namespace Scobius.Services;

public class UserService(ScobiusTest_Context context)
{


    public async Task<UserDto> CompleteRegistration(string uid)
    {
        UserRecord userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);

        if (!userRecord.EmailVerified)
        {
            throw new Exception("Email not verified.");
        }

        if (await context.Users.AnyAsync(u => u.Id == uid))
        {
            throw new Exception("User already registered.");
        }

        User user = new User()
        {
            Id = userRecord.Uid,
            Username = userRecord.DisplayName ?? $"User{context.Users.Count()}",
            ProfilePicture = userRecord.PhotoUrl,
            Email = userRecord.Email,
            LastSeen = DateTime.UtcNow,
            CreateAt = DateTime.UtcNow
        };

        var entry = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        return DtoHandler.ToUserDto(entry.Entity);
    }


    public async Task<bool> Exists(String userId) => await context.Users.AnyAsync((u) => u.Id == userId);

    public async Task DeleteUser(String userId)
    {
        try
        {
            await FirebaseAuth.DefaultInstance.DeleteUserAsync(userId);
            User? user = await context.Users.FindAsync(userId);
            if (user == null) return;
            context.Users.Remove(user);
            await context.SaveChangesAsync();

        }
        catch (System.Exception)
        {
            throw;
        }

    }
    public async Task RetrieveUser()
    {
        try
        {

            var verifiedUsers = await GetVerifiedUsersAsync();
            await StoreUsersInDatabaseAsync(verifiedUsers);
            Console.WriteLine("Successfully fetched and stored verified users.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching users: {ex.Message}");
        }
    }
    async Task<List<UserRecord>> GetVerifiedUsersAsync()
    {
        var users = new List<UserRecord>();

        var pagedEnumerable = FirebaseAuth.DefaultInstance.ListUsersAsync(null);
        var enumerator = pagedEnumerable.GetAsyncEnumerator();

        try
        {
            while (await enumerator.MoveNextAsync())
            {
                var user = enumerator.Current;
                if (user.EmailVerified)
                {
                    users.Add(user);
                }
            }
        }
        finally
        {
            await enumerator.DisposeAsync();
        }

        return users;
    }

    async Task StoreUsersInDatabaseAsync(List<UserRecord> users)
    {

        using var transaction = await context.Database.BeginTransactionAsync();
        foreach (var user in users)
        {
            if ((await Exists(user.Uid))) continue;
            User entity = new()
            {
                Id = user.Uid,
                Username = user.DisplayName ?? $"User{context.Users.Count()}",
                ProfilePicture = user.PhotoUrl,
                Email = user.Email,
                LastSeen = DateTime.UtcNow,
                CreateAt = DateTime.UtcNow
            };
            await context.Users.AddAsync(entity);
            await context.SaveChangesAsync();
        }
        await transaction.CommitAsync();
    }
}
