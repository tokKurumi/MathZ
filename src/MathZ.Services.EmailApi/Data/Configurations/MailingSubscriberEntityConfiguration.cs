namespace MathZ.Services.EmailApi.Data.Configurations;

using MathZ.Services.EmailApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MailingSubscriberEntityConfiguration : IEntityTypeConfiguration<MailingSubscriber>
{
    public void Configure(EntityTypeBuilder<MailingSubscriber> builder)
    {
        builder.HasKey(ms => ms.Id);

        builder.HasIndex(ms => new { ms.Email, ms.MailingId });

        builder
            .HasOne(ms => ms.Mailing)
            .WithMany(m => m.Subscribers)
            .HasForeignKey(ms => ms.MailingId);
    }
}
