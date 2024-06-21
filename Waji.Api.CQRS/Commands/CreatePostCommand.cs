using MediatR;
using Waji.Api.Shared.Response;

namespace Waji.Api.CQRS.Commands
{
    public class CreatePostCommand : IRequest<BaseResponse<PostCreationResponse>>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string DatePublished { get; set; }
        public int BlogId { get; set; }
    }
}
