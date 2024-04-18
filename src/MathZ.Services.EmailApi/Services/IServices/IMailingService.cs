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
    /// <summary>
    /// Creates a new mailing with the specified topic and optional description.
    /// </summary>
    /// <param name="topic">The topic of the mailing.</param>
    /// <param name="description">The optional description of the mailing.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CreateMailingAsync(string topic, string? description, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a mailing by its ID.
    /// </summary>
    /// <param name="id">The ID of the mailing to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<Result> DeleteMailingByIdAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a mailing by its ID.
    /// </summary>
    /// <param name="id">The ID of the mailing to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<Result<MailingDto>> GetMailingByIdAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a mailing by its topic.
    /// </summary>
    /// <param name="topic">The topic of the mailing to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<Result<MailingDto>> GetMailingByTopicAsync(string topic, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a queryable list of mailing emails.
    /// </summary>
    /// <returns>A queryable list of mailing emails.</returns>
    IQueryable<MailingEmailDto> GetMailingEmails();

    /// <summary>
    /// Sends an email using the mailing ID, subject, and body.
    /// </summary>
    /// <param name="mailingId">The ID of the mailing to send the email for.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body of the email.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<Result<IEnumerable<string>>> SendEmailByIdAsync(string mailingId, string subject, string body, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a mailing by its ID.
    /// </summary>
    /// <param name="id">The ID of the mailing to update.</param>
    /// <param name="patch">The JSON patch document containing the updates.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task<Result> UpdateMailingByIdAsync(string id, JsonPatchDocument<MailingPatch> patch, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a queryable list of mailings.
    /// </summary>
    /// <returns>A queryable list of mailings.</returns>
    IQueryable<MailingDto> GetMailings();
}
