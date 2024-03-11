namespace MathZ.Services.EmailApi.Services;

using System.Threading.Tasks;
using AutoMapper;
using MailKit.Net.Smtp;
using MathZ.Services.EmailApi.Models;
using MathZ.Services.EmailApi.Services.IServices;
using MathZ.Shared;
using MimeKit;
using MimeKit.Text;

public class EmailSenderService(
    IConfiguration configuration,
    IMapper mapper,
    SmtpClient smtpClient)
    : IEmailSenderService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IMapper _mapper = mapper;
    private readonly SmtpClient _smtpClient = smtpClient;

    public async Task SendEmailAsync(IEnumerable<MailAddress> to, string subject, string body, CancellationToken cancellationToken = default)
    {
        var from = _configuration.GetValue<string>(SmtpEnvConstants.From) ?? string.Empty;
        var message = new MimeMessage
        {
            Subject = subject,
            Body = new TextPart(TextFormat.Html)
            {
                Text = body,
            },
        };
        message.From.Add(new MailboxAddress(string.Empty, from));
        message.To.AddRange(_mapper.Map<IEnumerable<MailboxAddress>>(to));

        await _smtpClient.SendAsync(message, cancellationToken);
    }
}
