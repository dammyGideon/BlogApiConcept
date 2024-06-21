using MediatR;
using Waji.Api.Shared.Response;

namespace Waji.Api.CQRS.Commands
{
    public class RegisterAuthorCommand : IRequest<BaseResponse<AuthorResponse>>
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
