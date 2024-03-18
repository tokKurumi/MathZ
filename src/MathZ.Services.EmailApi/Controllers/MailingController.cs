namespace MathZ.Services.EmailApi.Controllers;

using Asp.Versioning;
using MathZ.Services.EmailApi.Models;
using MathZ.Services.EmailApi.Services.IServices;
using MathZ.Shared.Pagination;
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMailingByIdAsync([FromRoute] string id, CancellationToken cancellationToken)
    {
        var mailingResult = await _mailingService.GetMailingByIdAsync(id, cancellationToken);

        return mailingResult.IsSuccess ? Ok(mailingResult.Value) : StatusCode(500, mailingResult.Errors);
    }

    [HttpGet]
    public async Task<IActionResult> GetMailingByTopicAsync([FromQuery] string topic, CancellationToken cancellationToken)
    {
        var mailingResult = await _mailingService.GetMailingByTopicAsync(topic, cancellationToken);

        return mailingResult.IsSuccess ? Ok(mailingResult.Value) : StatusCode(500, mailingResult.Errors);
    }

    [HttpGet]
    public async Task<IActionResult> GetMailingsAsync([FromQuery] PaginationRequest pagination, CancellationToken cancellationToken)
    {
        var mailings = await _mailingService.GetMailingsAsync(pagination.PageNumber - 1, pagination.PageSize, cancellationToken);

        return Ok(mailings);
    }

    [HttpGet("{id}/Emails")]
    public async Task<IActionResult> GetMailingEmailsByIdAsync([FromRoute] string id, [FromQuery] PaginationRequest pagination, CancellationToken cancellationToken)
    {
        var emails = await _mailingService.GetMailingEmailsByIdAsync(id, pagination.PageNumber - 1, pagination.PageSize, cancellationToken);

        return Ok(emails);
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateMailingByIdAsync([FromRoute] string id, [FromBody] JsonPatchDocument<MailingPatch> patch, CancellationToken cancellationToken)
    {
        var updateResult = await _mailingService.UpdateMailingByIdAsync(id, patch, cancellationToken);

        return updateResult.IsSuccess ? Ok() : StatusCode(500, updateResult.Errors);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteMailingByIdAsync([FromRoute] string id, CancellationToken cancellationToken)
    {
        var deleteResult = await _mailingService.DeleteMailingByIdAsync(id, cancellationToken);

        return deleteResult.IsSuccess ? Ok() : StatusCode(500, deleteResult.Errors);
    }
}
