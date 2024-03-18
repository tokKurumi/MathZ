namespace MathZ.Services.EmailApi.Models.Dtos;

using System.ComponentModel;

[DisplayName("MailingEmailResponse")]
public record MailingEmailDto(
    string Id,
    string MailingId,
    string Subject,
    string Body,
    DateTime Created);
