namespace MathZ.Services.ForumApi.Data.Configurations;

using MathZ.Services.ForumApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MessageDislikeEntityConfiguration : IEntityTypeConfiguration<MessageDislike>
{
    public void Configure(EntityTypeBuilder<MessageDislike> builder)
    {
        builder.HasKey(md => md.Id);

        builder
            .HasOne(md => md.Message)
            .WithMany(m => m.Dislikes)
            .HasForeignKey(md => md.MessageId);
    }
}
