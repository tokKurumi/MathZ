namespace MathZ.Services.EmailApi.Features.Queries.GetMailingSubscribersById;

using MathZ.Shared.Pagination;

public class GetMailingSubscribersByIdCommand
    : PaginationQuery<string>
{
    public GetMailingSubscribersByIdCommand()
        : base()
    {
        MailingId = string.Empty;
    }

    public GetMailingSubscribersByIdCommand(string mailingId, PaginationQuery<string> pagination)
    {
        MailingId = mailingId;

        PageNumber = pagination.PageNumber;
        PageSize = pagination.PageSize;
    }

    public string MailingId { get; set; }
}
