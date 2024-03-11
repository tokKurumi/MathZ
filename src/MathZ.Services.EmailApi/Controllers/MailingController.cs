namespace MathZ.Services.EmailApi.Controllers;

using System.Security.Claims;
using Asp.Versioning;
using MathZ.Services.EmailApi.Models;
using MathZ.Services.EmailApi.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[ApiVersion(1.0)]
[Route("v{version:apiVersion}/[controller]")]
public class MailingController(
    IMailingService mailingService)
    : ControllerBase
{
    private readonly IMailingService _mailingService = mailingService;

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> CreateMailingAsync([FromQuery] string topic, [FromQuery] string? description, CancellationToken cancellationToken)
    {
        await _mailingService.CreateMailingAsync(topic, description, cancellationToken);

        return Ok();
    }

    [HttpGet("Id/{id}")]
    public async Task<IActionResult> GetMailingByIdAsync([FromRoute] string id, CancellationToken cancellationToken)
    {
        var mailingResult = await _mailingService.GetMailingByIdAsync(id, cancellationToken);

        return mailingResult.IsSuccess ? Ok(mailingResult.Value) : StatusCode(500, mailingResult.Errors);
    }

    [HttpGet("Topic/{topic}")]
    public async Task<IActionResult> GetMailingByTopicAsync([FromRoute] string topic, CancellationToken cancellationToken)
    {
        var mailingResult = await _mailingService.GetMailingByTopicAsync(topic, cancellationToken);

        return mailingResult.IsSuccess ? Ok(mailingResult.Value) : StatusCode(500, mailingResult.Errors);
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetMailingsAsync([FromQuery] int skip, [FromQuery] int count, CancellationToken cancellationToken)
    {
        var mailings = await _mailingService.GetMailingsAsync(skip, count, cancellationToken);

        return Ok(mailings);
    }

    [HttpPut("Id/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateMailingByIdAsync([FromRoute] string id, [FromBody] JsonPatchDocument<MailingPatch> patch, CancellationToken cancellationToken)
    {
        var updateResult = await _mailingService.UpdateMailingByIdAsync(id, patch, cancellationToken);

        return updateResult.IsSuccess ? Ok() : StatusCode(500, updateResult.Errors);
    }

    [HttpDelete("Id/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteMailingByIdAsync([FromRoute] string id, CancellationToken cancellationToken)
    {
        var deleteResult = await _mailingService.DeleteMailingByIdAsync(id, cancellationToken);

        return deleteResult.IsSuccess ? Ok() : StatusCode(500, deleteResult.Errors);
    }

    [HttpPost("Subscribers/{topic}")]
    [Authorize]
    public async Task<IActionResult> SubscribeMailingByTopicAsync([FromRoute] string topic, CancellationToken cancellationToken)
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        if (email is null)
        {
            return StatusCode(500, "Something went wrong. Please, contact administrator.");
        }

        var subscribeResult = await _mailingService.SubscribeMailingByTopicAsync(email, topic, cancellationToken);

        return subscribeResult.IsSuccess ? Ok() : StatusCode(500, subscribeResult.Errors);
    }

    [HttpGet("Subscribers/{topic}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> GetMailingTopicSubscribersAsync([FromRoute] string topic, [FromQuery] int skip, [FromQuery] int count, CancellationToken cancellationToken)
    {
        var subscribersResult = await _mailingService.GetMailingTopicSubscribersAsync(topic, skip, count, cancellationToken);

        return subscribersResult.IsSuccess ? Ok(subscribersResult.Value) : StatusCode(500, subscribersResult.Errors);
    }

    [HttpDelete("Subscribers/{topic}")]
    [Authorize]
    public async Task<IActionResult> UnsubscribeMailingByTopicAsync([FromRoute] string topic, CancellationToken cancellationToken)
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;

        if (email is null)
        {
            return StatusCode(500, "Something went wrong. Please, contact administrator.");
        }

        var unsubscribeResult = await _mailingService.UnsubscribeMailingByTopicAsync(email, topic, cancellationToken);

        return unsubscribeResult.IsSuccess ? Ok() : StatusCode(500, unsubscribeResult.Errors);
    }
}
