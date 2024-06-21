using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Waje.Api.Data.Contract;
using Waje.Api.Data.UnitOfWork;
using Waji.Api.CQRS.Commands;
using Waji.Api.Data.Models;
using Waji.Api.Shared.Response;

namespace Waji.Api.CQRS.Handlers.CommandHandlers
{
    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, BaseResponse<BlogCreationReponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateBlogCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse<BlogCreationReponse>> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            var requestHost = _httpContextAccessor.HttpContext.Request.Host;
            var requestScheme = _httpContextAccessor.HttpContext.Request.Scheme;
            var blogUrl = $"{requestScheme}://{requestHost}/{request.BlogName.ToLower().Replace(' ', '-')}";

            var authorExist = await _unitOfWork.GetRepository<Author>().GetByIdAsync(d => d.Id == request.AuthorId, false);

            if (!authorExist.Any())
            {
                return new BaseResponse<BlogCreationReponse> { Success = false, Message = "Author with this Id Does not Exist", StatusCode = HttpStatusCode.NotFound };
            };

            Blog blogresponse = new()
            {
                Name = request.BlogName,
                AuthorId = request.AuthorId,
                Url = blogUrl,
            };
            await _unitOfWork.GetRepository<Blog>().Create(blogresponse);
            await _unitOfWork.SaveChangesAsync();

            BlogCreationReponse blogCreationReponse = new()
            {
                BlogName = blogresponse.Name,
                Url = blogresponse.Url,
            };
            return new BaseResponse<BlogCreationReponse> { Success = true, Message = "Blog created successfully", Data = blogCreationReponse, StatusCode = HttpStatusCode.OK };


        }
    }
}
