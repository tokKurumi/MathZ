namespace MathZ.Services.EmailApi.Data;

using MathZ.Services.EmailApi.Data.Configurations;
using MathZ.Services.EmailApi.Entities;
using Microsoft.EntityFrameworkCore;

public class MailingDbContext(DbContextOptions options)
    : DbContext(options)
{
    public DbSet<Mailing> Mailings { get; set; }

    public DbSet<MailingSubscriber> Subscribers { get; set; }

    public DbSet<MailingEmail> Emails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new MailingEntityConfiguration())
            .ApplyConfiguration(new MailingSubscriberEntityConfiguration())
            .ApplyConfiguration(new MailingEmailEntityConfiguration());
    }
}
