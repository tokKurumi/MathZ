namespace MathZ.Services.EmailAPI.Services;

using AutoMapper;
using MailKit.Net.Smtp;
using MathZ.Services.EmailAPI.Services.IServices;
using MathZ.Shared.Models;
using MimeKit;

public class EmailService(
    SmtpClient smtp,
    HttpClient userApi,
    IMapper mapper)
    : IEmailService
{
    private readonly SmtpClient _smtp = smtp;
    private readonly IMapper _mapper = mapper;

    public async Task SendMessageAsync(Email email)
    {
        await _smtp.SendAsync(_mapper.Map<MimeMessage>(email));
    }
}