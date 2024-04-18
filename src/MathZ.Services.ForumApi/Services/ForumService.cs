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

        await _dbContext.Messages.AddAsync(messageEntity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<MessageDto> GetMessages()
    {
        return _mapper.ProjectTo<MessageDto>(_dbContext.Messages.AsNoTracking().OrderBy(m => m.Created));
    }

    public async Task LikeMessageAsync(MessageLikeDto messageLike, CancellationToken cancellationToken = default)
    {
        var like = _mapper.Map<MessageLike>(messageLike);

        await _dbContext.Likes.AddAsync(like, cancellationToken);
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

        await _dbContext.Dislikes.AddAsync(dislike, cancellationToken);
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
