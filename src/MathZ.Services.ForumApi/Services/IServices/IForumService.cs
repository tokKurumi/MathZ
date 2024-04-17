namespace MathZ.Services.ForumApi.Services.IServices;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MathZ.Services.ForumApi.Models.Dtos;

public interface IForumService
{
    Task DislikeMessageAsync(MessageDislikeDto messageDislike, CancellationToken cancellationToken = default);

    IQueryable<MessageDislikeDto> GetDislikes();

    IQueryable<MessageLikeDto> GetLikes();

    IQueryable<MessageDto> GetMessages();

    Task LikeMessageAsync(MessageLikeDto messageLike, CancellationToken cancellationToken = default);

    Task SendMessageAsync(MessageDto message, CancellationToken cancellationToken = default);
}