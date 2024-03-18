namespace MathZ.Services.EmailApi.Services;

using AutoMapper;
using FluentResults;
using MathZ.Services.EmailApi.Data;
using MathZ.Services.EmailApi.Entities;
using MathZ.Services.EmailApi.Models.Dtos;
using MathZ.Services.EmailApi.Services.IServices;
using Microsoft.EntityFrameworkCore;

public class SubscriptionService(
    IMapper mapper,
    MailingDbContext mailingDbContext)
    : ISubscriptionService
{
    private readonly IMapper _mapper = mapper;
    private readonly MailingDbContext _mailingDbContext = mailingDbContext;

    public async Task<Result> SubscribeMailingByIdAsync(string mailingId, string email, CancellationToken cancellationToken = default)
    {
        var mailing = await _mailingDbContext.Mailings
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == mailingId, cancellationToken);

        if (mailing is null)
        {
            return Result.Fail("Could not find mailing with provided topic");
        }

        var subscription = new MailingSubscriber
        {
            Email = email,
            MailingId = mailing.Id,
        };

        await _mailingDbContext.Subscribers.AddAsync(subscription, cancellationToken);
        await _mailingDbContext.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }

    public async Task<IEnumerable<string>> GetMailingSubscribersByIdAsync(string mailingId, int skip, int count, CancellationToken cancellationToken = default)
    {
        return await _mailingDbContext.Subscribers
            .AsNoTracking()
            .Where(ms => ms.MailingId == mailingId)
            .Skip(skip)
            .Take(count)
            .Select(ms => ms.Email)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetMailingSubscribersCountByIdAsync(string mailingId, CancellationToken cancellationToken = default)
    {
        return await _mailingDbContext.Subscribers
            .AsNoTracking()
            .CountAsync(ms => ms.MailingId == mailingId, cancellationToken);
    }

    public async Task<IEnumerable<MailingDto>> GetSubscribedMailingsByEmailAsync(string email, int skip, int count, CancellationToken cancellationToken = default)
    {
        return await _mapper.ProjectTo<MailingDto>(_mailingDbContext.Subscribers
            .AsNoTracking()
            .Include(ms => ms.Mailing)
            .Where(ms => ms.Email == email)
            .Skip(skip)
            .Take(count)
            .Select(ms => ms.Mailing))
            .ToListAsync(cancellationToken);
    }

    public async Task<int> GetSubscribedMailingsCountByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _mailingDbContext.Subscribers
            .AsNoTracking()
            .CountAsync(ms => ms.Email == email, cancellationToken);
    }

    public async Task<Result> UnsubscribeMailingByIdAsync(string mailingId, string email, CancellationToken cancellationToken = default)
    {
        var mailing = await _mailingDbContext.Mailings
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == mailingId, cancellationToken);

        if (mailing is null)
        {
            return Result.Fail("Could not find mailing with provided id");
        }

        var subscription = await _mailingDbContext.Subscribers
            .FirstOrDefaultAsync(ms => ms.MailingId == mailing.Id && ms.Email == email, cancellationToken);

        if (subscription is null)
        {
            return Result.Fail("Could not find subscription with provided mailingId and email");
        }

        _mailingDbContext.Subscribers.Remove(subscription);
        await _mailingDbContext.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
