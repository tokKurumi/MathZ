namespace MathZ.Services.EmailApi.Features.Queries.GetSubscribedMailingsByEmail;

using MathZ.Services.EmailApi.Models.Dtos;
using MathZ.Shared.Pagination;

public class GetSubscribedMailingsByEmailQuery
    : PaginationQuery<MailingDto>
{
    public GetSubscribedMailingsByEmailQuery()
        : base()
    {
        Email = string.Empty;
    }

    public GetSubscribedMailingsByEmailQuery(string email, PaginationQuery<MailingDto> pagination)
    {
        Email = email;

        PageNumber = pagination.PageNumber;
        PageSize = pagination.PageSize;
    }

    public string Email { get; set; }
}
