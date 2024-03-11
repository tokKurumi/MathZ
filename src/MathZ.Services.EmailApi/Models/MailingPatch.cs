namespace MathZ.Services.EmailApi.Models;

public class MailingPatch
{
    public string Topic { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;
}
