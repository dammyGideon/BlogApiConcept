using MediatR;
using System.Net;
using Waje.Api.Data.UnitOfWork;
using Waji.Api.CQRS.Commands;
using Waji.Api.Data.Models;
using Waji.Api.Shared.Response;

namespace Waji.Api.CQRS.Handlers.CommandHandlers
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, BaseResponse<PostCreationResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePostCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<BaseResponse<PostCreationResponse>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var postExist = await _unitOfWork.GetRepository<Blog>().GetByIdAsync(d => d.Id == request.BlogId, false);
            if (postExist == null)
            {
                return new BaseResponse<PostCreationResponse>
                {
                    Success = false,
                    Message = $"Blog with this Id {request.BlogId} does not exist",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            Post post = new()
            {
                Title = request.Title,
                Content = request.Content,
                BlogId = request.BlogId,
                DatePublished = request.DatePublished,

            };
            await _unitOfWork.GetRepository<Post>().Create(post);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponse<PostCreationResponse> { Success = true, Message = "Post created Successfully ", StatusCode = HttpStatusCode.OK };
        }
    }
}
