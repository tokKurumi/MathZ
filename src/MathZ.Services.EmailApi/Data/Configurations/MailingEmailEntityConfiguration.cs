namespace MathZ.Services.EmailApi.Data.Configurations;

using MathZ.Services.EmailApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MailingEmailEntityConfiguration : IEntityTypeConfiguration<MailingEmail>
{
    public void Configure(EntityTypeBuilder<MailingEmail> builder)
    {
        builder.HasKey(me => me.Id);

        builder
            .HasOne(me => me.Mailing)
            .WithMany(m => m.Emails)
            .HasForeignKey(me => me.MailingId);
    }
}
