namespace MathZ.Services.ForumApi.Data;

using MathZ.Services.ForumApi.Data.Configurations;
using MathZ.Services.ForumApi.Entities;
using Microsoft.EntityFrameworkCore;

public class ForumDbContext(DbContextOptions options)
    : DbContext(options)
{
    public DbSet<Message> Messages { get; set; }

    public DbSet<MessageDislike> Dislikes { get; set; }

    public DbSet<MessageLike> Likes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new MessageEntityConfiguration())
            .ApplyConfiguration(new MessageDislikeEntityConfiguration())
            .ApplyConfiguration(new MessageLikeEntityConfiguration());
    }
}
