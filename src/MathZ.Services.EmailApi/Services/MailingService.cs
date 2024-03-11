namespace MathZ.Services.EmailApi.Services;

using System.Threading;
using AutoMapper;
using FluentResults;
using MathZ.Services.EmailApi.Data;
using MathZ.Services.EmailApi.Entities;
using MathZ.Services.EmailApi.Models;
using MathZ.Services.EmailApi.Models.Dtos;
using MathZ.Services.EmailApi.Services.IServices;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

public class MailingService(
    IMapper mapper,
    MailingDbContext mailingDbContext)
    : IMailingService
{
    private readonly IMapper _mapper = mapper;
    private readonly MailingDbContext _mailingDbContext = mailingDbContext;

    public async Task CreateMailingAsync(string topic, string? description, CancellationToken cancellationToken = default)
    {
        var mailing = new Mailing
        {
            Topic = topic,
            Description = description,
        };

        await _mailingDbContext.Mailings.AddAsync(mailing, cancellationToken);
        await _mailingDbContext.SaveChangesAsync(cancellationToken);

        await Console.Out.WriteLineAsync(mailing.Id);
    }

    public async Task<Result<MailingDto>> GetMailingByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var mailing = await _mailingDbContext.Mailings
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

        return mailing switch
        {
            not null => Result.Ok(_mapper.Map<MailingDto>(mailing)),
            _ => Result.Fail("Could not find mailing with provided id"),
        };
    }

    public async Task<Result<MailingDto>> GetMailingByTopicAsync(string topic, CancellationToken cancellationToken = default)
    {
        var mailing = await _mailingDbContext.Mailings
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Topic == topic, cancellationToken);

        return mailing switch
        {
            not null => Result.Ok(_mapper.Map<MailingDto>(mailing)),
            _ => Result.Fail("Could not find mailing with provided topic"),
        };
    }

    public async Task<IEnumerable<MailingDto>> GetMailingsAsync(int skip, int count, CancellationToken cancellationToken = default)
    {
        return await _mapper.ProjectTo<MailingDto>(_mailingDbContext.Mailings
            .AsNoTracking()
            .Skip(skip)
            .Take(count))
            .ToListAsync(cancellationToken);
    }

    public async Task<Result> UpdateMailingByIdAsync(string id, JsonPatchDocument<MailingPatch> patch, CancellationToken cancellationToken = default)
    {
        var mailing = await _mailingDbContext.Mailings
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

        if (mailing is null)
        {
            return Result.Fail("Could not find mailing with provided id");
        }

        var patched = _mapper.Map<MailingPatch>(mailing);
        patch.ApplyTo(patched);

        mailing.Topic = patched.Topic;
        mailing.Description = patched.Description;
        await _mailingDbContext.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }

    public async Task<Result> DeleteMailingByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var mailing = await _mailingDbContext.Mailings
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

        if (mailing is null)
        {
            return Result.Fail("Could not find mailing with provided id");
        }

        _mailingDbContext.Mailings.Remove(mailing);
        await _mailingDbContext.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }

    public async Task<Result> SubscribeMailingByTopicAsync(string email, string topic, CancellationToken cancellationToken = default)
    {
        var mailing = await _mailingDbContext.Mailings
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Topic == topic, cancellationToken);

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

    public async Task<Result<IEnumerable<string>>> GetMailingTopicSubscribersAsync(string topic, int skip, int count, CancellationToken cancellationToken = default)
    {
        var mailing = await _mailingDbContext.Mailings
            .AsNoTracking()
            .Include(m => m.Subscribers)
            .FirstOrDefaultAsync(m => m.Topic == topic, cancellationToken);

        if (mailing is null)
        {
            return Result.Fail("Could not find mailing with provided topic");
        }

        return Result.Ok(mailing.Subscribers
            .Skip(skip)
            .Take(count)
            .Select(ms => ms.Email));
    }

    public async Task<Result> UnsubscribeMailingByTopicAsync(string email, string topic, CancellationToken cancellationToken = default)
    {
        var mailing = await _mailingDbContext.Mailings
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Topic == topic, cancellationToken);

        if (mailing is null)
        {
            return Result.Fail("Could not find mailing with provided topic");
        }

        var subscription = await _mailingDbContext.Subscribers
            .AsNoTracking()
            .FirstOrDefaultAsync(ms => ms.MailingId == mailing.Id && ms.Email == email, cancellationToken);

        if (subscription is null)
        {
            return Result.Fail("Could not find subscription on this topic");
        }

        _mailingDbContext.Subscribers.Remove(subscription);
        await _mailingDbContext.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }

    public async Task<Result<IEnumerable<string>>> SendEmailToTopicAsync(string subject, string body, string topic, CancellationToken cancellationToken = default)
    {
        var mailing = await _mailingDbContext.Mailings
            .AsNoTracking()
            .Include(m => m.Subscribers)
            .FirstOrDefaultAsync(m => m.Topic == topic, cancellationToken);

        if (mailing is null)
        {
            return Result.Fail("Could not find mailing with provided topic");
        }

        var mailingEmail = new MailingEmail
        {
            MailingId = mailing.Id,
            Subject = subject,
            Body = body,
        };

        await _mailingDbContext.Emails.AddAsync(mailingEmail, cancellationToken);
        await _mailingDbContext.SaveChangesAsync(cancellationToken);

        return Result.Ok(mailing.Subscribers.Select(ms => ms.Email));
    }
}
