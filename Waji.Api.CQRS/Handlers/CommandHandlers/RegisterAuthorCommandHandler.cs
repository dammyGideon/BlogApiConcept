using MediatR;
using System.Net;
using Waje.Api.Data.UnitOfWork;
using Waji.Api.CQRS.Commands;
using Waji.Api.Data.Models;
using Waji.Api.Shared.Response;

namespace Waji.Api.CQRS.Handlers.CommandHandlers
{
    public class RegisterAuthorCommandHandler : IRequestHandler<RegisterAuthorCommand, BaseResponse<AuthorResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegisterAuthorCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<BaseResponse<AuthorResponse>> Handle(RegisterAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorExist = await _unitOfWork.GetRepository<Author>().GetByIdAsync(d => d.Name == request.Name, false);
            if (authorExist.Any())
            {
                return new BaseResponse<AuthorResponse> { Success = false, Message = "", StatusCode = HttpStatusCode.Conflict };
            };

            Author author = new()
            {
                Name = request.Name,
                Email = request.Email,
            };
            await _unitOfWork.GetRepository<Author>().Create(author);
            await _unitOfWork.SaveChangesAsync();

            AuthorResponse response = new()
            {
                Name = author.Name,
                Email = author.Email,
            };
            return new BaseResponse<AuthorResponse> { Success = true, Message = "", Data = response, StatusCode = HttpStatusCode.OK };
        }
    }
}
