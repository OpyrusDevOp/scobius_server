using Microsoft.EntityFrameworkCore;
using Scobius.Entities;

namespace Scobius;

public class ScobiusTest_Context(DbContextOptions<ScobiusTest_Context> options) : DbContext(options)
{
    public DbSet<Frienship> Frienships { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Media> Medias { get; set; }
    public DbSet<User> Users { get; set; }
}
