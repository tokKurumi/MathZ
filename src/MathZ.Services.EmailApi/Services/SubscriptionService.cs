namespace MathZ.Services.EmailApi.Services;

using System.Linq;
using System.Threading;
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

        _mailingDbContext.Subscribers.Add(subscription);
        await _mailingDbContext.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }

    public IQueryable<MailingDto> GetSubscribedMailingsByEmail(string email)
    {
        return _mapper.ProjectTo<MailingDto>(_mailingDbContext.Subscribers
            .AsNoTracking()
            .Include(ms => ms.Mailing)
            .Where(ms => ms.Email == email)
            .Select(ms => ms.Mailing));
    }

    public IQueryable<string> GetMailingSubscribersById(string mailingId)
    {
        return _mailingDbContext.Subscribers
            .AsNoTracking()
            .Where(ms => ms.MailingId == mailingId)
            .Select(ms => ms.Email);
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
