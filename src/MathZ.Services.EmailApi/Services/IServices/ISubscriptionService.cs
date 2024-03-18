namespace MathZ.Services.EmailApi.Services.IServices;

using System.Collections.Generic;
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
    /// <param name="skip">The number of subscribers to skip.</param>
    /// <param name="count">The number of subscribers to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the mailing subscribers.</returns>
    Task<Result<IEnumerable<string>>> GetMailingSubscribersByIdAsync(string mailingId, int skip, int count, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the subscribed mailings by email.
    /// </summary>
    /// <param name="email">The email address.</param>
    /// <param name="skip">The number of mailings to skip.</param>
    /// <param name="count">The number of mailings to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the subscribed mailings.</returns>
    Task<IEnumerable<MailingDto>> GetSubscribedMailingsByEmailAsync(string email, int skip, int count, CancellationToken cancellationToken = default);

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
