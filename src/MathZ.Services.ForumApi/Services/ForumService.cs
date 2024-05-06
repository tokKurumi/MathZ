namespace MathZ.Services.ForumApi.Services;

using AutoMapper;
using MathZ.Services.ForumApi.Data;
using MathZ.Services.ForumApi.Entities;
using MathZ.Services.ForumApi.Models.Dtos;
using MathZ.Services.ForumApi.Services.IServices;
using Microsoft.EntityFrameworkCore;

public class ForumService(
    IMapper mapper,
    ForumDbContext dbContext)
    : IForumService
{
    private readonly IMapper _mapper = mapper;
    private readonly ForumDbContext _dbContext = dbContext;

    public async Task SendMessageAsync(MessageDto message, CancellationToken cancellationToken = default)
    {
        var messageEntity = _mapper.Map<Message>(message);

        _dbContext.Messages.Add(messageEntity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<MessageDto> GetMessages()
    {
        return _mapper.ProjectTo<MessageDto>(_dbContext.Messages.AsNoTracking().OrderBy(m => m.Created));
    }

    public async Task LikeMessageAsync(MessageLikeDto messageLike, CancellationToken cancellationToken = default)
    {
        var like = _mapper.Map<MessageLike>(messageLike);

        _dbContext.Likes.Add(like);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<MessageLikeDto> GetLikes(string messageId)
    {
        return _mapper.ProjectTo<MessageLikeDto>(
            _dbContext.Likes.AsNoTracking()
            .Where(m => m.MessageId == messageId)
            .OrderBy(m => m.Created));
    }

    public async Task DislikeMessageAsync(MessageDislikeDto messageDislike, CancellationToken cancellationToken = default)
    {
        var dislike = _mapper.Map<MessageDislike>(messageDislike);

        _dbContext.Dislikes.Add(dislike);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<MessageDislikeDto> GetDislikes(string messageId)
    {
        return _mapper.ProjectTo<MessageDislikeDto>(
            _dbContext.Dislikes.AsNoTracking()
            .Where(m => m.MessageId == messageId)
            .OrderBy(m => m.Created));
    }
}
