namespace MathZ.Services.ForumApi.Data.Configurations;

using MathZ.Services.ForumApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MessageEntityConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(m => m.Id);

        builder
            .HasMany(m => m.Replies)
            .WithOne()
            .HasForeignKey(m => m.ParentMessageId)
            .IsRequired(false);

        builder
            .HasMany(m => m.Likes)
            .WithOne(ml => ml.Message);

        builder
            .HasMany(m => m.Dislikes)
            .WithOne(md => md.Message);
    }
}
