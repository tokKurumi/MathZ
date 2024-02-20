namespace MathZ.Services.EmailAPI;

using AutoMapper;
using MathZ.Shared.Models;
using MimeKit;

public static class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        return new MapperConfiguration(config =>
        {
            config.CreateMap<MailBoxAddress, MailboxAddress>()
                .ConvertUsing(converter => new MailboxAddress(
                    converter.Name,
                    converter.Address));

            config.CreateMap<Email, MimeMessage>()
                .ConvertUsing((x1, _) =>
                {
                    var msg = new MimeMessage();
                    msg.From.AddRange(x1.From.Select(address => new MailboxAddress(address.Name, address.Address)));
                    msg.To.AddRange(x1.To.Select(address => new MailboxAddress(address.Name, address.Address)));
                    msg.Subject = x1.Subject;
                    msg.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                    {
                        Text = x1.Body,
                    };
                    return msg;
                });
        });
    }
}