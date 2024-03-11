namespace MathZ.Services.EmailApi.Services.IServices;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using MathZ.Services.EmailApi.Models;
using MathZ.Services.EmailApi.Models.Dtos;
using Microsoft.AspNetCore.JsonPatch;

public interface IMailingService
{
    Task CreateMailingAsync(string topic, string? description, CancellationToken cancellationToken = default);

    Task<Result> DeleteMailingByIdAsync(string id, CancellationToken cancellationToken = default);

    Task<Result<MailingDto>> GetMailingByIdAsync(string id, CancellationToken cancellationToken = default);

    Task<Result<MailingDto>> GetMailingByTopicAsync(string topic, CancellationToken cancellationToken = default);

    Task<IEnumerable<MailingDto>> GetMailingsAsync(int skip, int count, CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<string>>> SendEmailToTopicAsync(string subject, string body, string topic, CancellationToken cancellationToken = default);

    Task<Result> SubscribeMailingByTopicAsync(string email, string topic, CancellationToken cancellationToken = default);

    Task<Result<IEnumerable<string>>> GetMailingTopicSubscribersAsync(string topic, int skip, int count, CancellationToken cancellationToken = default);

    Task<Result> UnsubscribeMailingByTopicAsync(string email, string topic, CancellationToken cancellationToken = default);

    Task<Result> UpdateMailingByIdAsync(string id, JsonPatchDocument<MailingPatch> patch, CancellationToken cancellationToken = default);
}