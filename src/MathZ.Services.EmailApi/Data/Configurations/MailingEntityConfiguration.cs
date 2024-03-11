namespace MathZ.Services.EmailApi.Data.Configurations;

using MathZ.Services.EmailApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MailingEntityConfiguration : IEntityTypeConfiguration<Mailing>
{
    public void Configure(EntityTypeBuilder<Mailing> builder)
    {
        builder.HasKey(m => m.Id);

        builder.HasIndex(m => m.Topic).IsUnique();

        builder
            .HasMany(m => m.Subscribers)
            .WithOne(ms => ms.Mailing);

        builder
            .HasMany(m => m.Emails)
            .WithOne(me => me.Mailing);
    }
}
