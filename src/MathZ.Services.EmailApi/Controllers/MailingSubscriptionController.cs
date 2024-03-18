namespace MathZ.Services.EmailApi.Controllers;

using System.Security.Claims;
using Asp.Versioning;
using MathZ.Services.EmailApi.Models.Dtos;
using MathZ.Services.EmailApi.Services.IServices;
using MathZ.Shared.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion(1.0)]
[Route("v{version:apiVersion}/[controller]")]
public class MailingSubscriptionController(
    ISubscriptionService subscriptionService)
    : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService;

    [HttpPost("{mailingId}")]
    [Authorize]
    public async Task<IActionResult> SubscribeMailingByIdAsync([FromRoute] string mailingId, CancellationToken cancellationToken)
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        if (email is null)
        {
            return StatusCode(500, "Something went wrong. Please, contact administrator.");
        }

        var result = await _subscriptionService.SubscribeMailingByIdAsync(mailingId, email, cancellationToken);

        return result.IsSuccess ? Ok() : StatusCode(500, result.Errors);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetSubscribedMailingsByEmailAsync([FromQuery] PaginationRequest pagination, CancellationToken cancellationToken)
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        if (email is null)
        {
            return StatusCode(500, "Something went wrong. Please, contact administrator.");
        }

        var mailings = await _subscriptionService.GetSubscribedMailingsByEmailAsync(email, (pagination.PageNumber - 1) * pagination.PageSize, pagination.PageSize, cancellationToken);
        var total = await _subscriptionService.GetSubscribedMailingsCountByEmailAsync(email, cancellationToken);

        return Ok(PaginationResponse<MailingDto>.Create(mailings, total, pagination.PageNumber));
    }

    [HttpGet("Subscribers/{mailingId}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetMailingSubscribersByIdAsync([FromRoute] string mailingId, [FromQuery] PaginationRequest pagination, CancellationToken cancellationToken)
    {
        var subs = await _subscriptionService.GetMailingSubscribersByIdAsync(mailingId, (pagination.PageNumber - 1) * pagination.PageSize, pagination.PageSize, cancellationToken);
        var total = await _subscriptionService.GetMailingSubscribersCountByIdAsync(mailingId, cancellationToken);

        return Ok(PaginationResponse<string>.Create(subs, total, pagination.PageNumber));
    }

    [HttpDelete("{mailingId}")]
    [Authorize]
    public async Task<IActionResult> UnsubscribeMailingByIdAsync([FromRoute] string mailingId, CancellationToken cancellationToken)
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        if (email is null)
        {
            return StatusCode(500, "Something went wrong. Please, contact administrator.");
        }

        var result = await _subscriptionService.UnsubscribeMailingByIdAsync(mailingId, email, cancellationToken);

        return result.IsSuccess ? Ok() : StatusCode(500, result.Errors);
    }
}
