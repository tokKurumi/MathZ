namespace MathZ.Services.EmailApi.Features.Queries.GetMailingSubscribersById;

using MathZ.Shared.Pagination;

public class GetMailingSubscribersByIdQuery
    : PaginationQuery<string>
{
    public GetMailingSubscribersByIdQuery()
        : base()
    {
        MailingId = string.Empty;
    }

    public GetMailingSubscribersByIdQuery(string mailingId, PaginationQuery<string> pagination)
    {
        MailingId = mailingId;

        PageNumber = pagination.PageNumber;
        PageSize = pagination.PageSize;
    }

    public string MailingId { get; set; }
}
