namespace MathZ.Services.ForumApi.Data.Configurations;

using MathZ.Services.ForumApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MessageLikeEntityConfiguration : IEntityTypeConfiguration<MessageLike>
{
    public void Configure(EntityTypeBuilder<MessageLike> builder)
    {
        builder.HasKey(ml => ml.Id);

        builder
            .HasOne(ml => ml.Message)
            .WithMany(m => m.Likes)
            .HasForeignKey(ml => ml.MessageId);
    }
}
