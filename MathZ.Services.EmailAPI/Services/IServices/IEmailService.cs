namespace MathZ.Services.EmailAPI.Services.IServices;

using MathZ.Shared.Models;

public interface IEmailService
{
    Task SendMessageAsync(Email email);
}