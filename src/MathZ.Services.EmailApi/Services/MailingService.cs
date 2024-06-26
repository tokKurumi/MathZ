﻿namespace MathZ.Services.EmailApi.Services;

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

        _mailingDbContext.Mailings.Add(mailing);
        await _mailingDbContext.SaveChangesAsync(cancellationToken);
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

    public IQueryable<MailingDto> GetMailings()
    {
        return _mapper.ProjectTo<MailingDto>(_mailingDbContext.Mailings.AsNoTracking().OrderBy(m => m.Id));
    }

    public IQueryable<MailingEmailDto> GetMailingEmails()
    {
        return _mapper.ProjectTo<MailingEmailDto>(_mailingDbContext.Emails.AsNoTracking().OrderBy(e => e.Id));
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

    public async Task<Result<IEnumerable<string>>> SendEmailByIdAsync(string mailingId, string subject, string body, CancellationToken cancellationToken = default)
    {
        var mailing = await _mailingDbContext.Mailings
            .AsNoTracking()
            .Include(m => m.Subscribers)
            .FirstOrDefaultAsync(m => m.Id == mailingId, cancellationToken);

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

        _mailingDbContext.Emails.Add(mailingEmail);
        await _mailingDbContext.SaveChangesAsync(cancellationToken);

        return Result.Ok(mailing.Subscribers.Select(ms => ms.Email));
    }
}
