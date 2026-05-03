using AutoMapper;
using MediatR;
using YouScout.Application.Common.Interfaces;
using YouScout.Application.Common.Models;
using YouScout.Application.Posts.Commands;
using YouScout.Domain.Common.Contracts;
using YouScout.Domain.Entities;
using YouScout.Infrastructure.Interfaces.Storage;

namespace YouScout.Application.Posts.Handlers;

public class CreatePostCommandHandler(
    IMediaStorageFactory storageFactory,
    IRepository<Post, Guid> repository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IUserContext currentUser)
    : IRequestHandler<CreatePostCommand, PostDto>
{
    private readonly IMediaStorage _storageProvider = storageFactory.Get(IMediaStorageFactory.StorageProvider.Local);

    public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        string url;
        await using (var stream = request.Video.OpenReadStream())
        {
            url = await this._storageProvider.UploadAsync(stream, Guid.CreateVersion7().ToString(),
                request.Video.ContentType, cancellationToken);
        }


        var post = Post.Create(
            currentUser.Id!,
            url,
            request.Description,
            request.Hashtags.ToList(),
            request.Skills.ToList(),
            request.Video.Length);

        await repository.AddAsync(post, cancellationToken);
        await unitOfWork.SaveChangeAsync(cancellationToken);

        return mapper.Map<PostDto>(post);
    }
}