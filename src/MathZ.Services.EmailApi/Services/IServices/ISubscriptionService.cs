namespace MathZ.Services.EmailApi.Services.IServices;

using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using MathZ.Services.EmailApi.Models.Dtos;

public interface ISubscriptionService
{
    /// <summary>
    /// Retrieves the mailing subscribers by mailing ID.
    /// </summary>
    /// <param name="mailingId">The ID of the mailing.</param>
    /// <returns>The mailing subscribers.</returns>
    IQueryable<string> GetMailingSubscribersById(string mailingId);

    /// <summary>
    /// Retrieves the subscribed mailings by email.
    /// </summary>
    /// <param name="email">The email address.</param>
    /// <returns>The subscribed mailings.</returns>
    IQueryable<MailingDto> GetSubscribedMailingsByEmail(string email);

    /// <summary>
    /// Subscribes to a mailing by mailing ID and email.
    /// </summary>
    /// <param name="mailingId">The ID of the mailing.</param>
    /// <param name="email">The email address.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result> SubscribeMailingByIdAsync(string mailingId, string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Unsubscribes from a mailing by mailing ID and email.
    /// </summary>
    /// <param name="mailingId">The ID of the mailing.</param>
    /// <param name="email">The email address.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<Result> UnsubscribeMailingByIdAsync(string mailingId, string email, CancellationToken cancellationToken = default);
}
