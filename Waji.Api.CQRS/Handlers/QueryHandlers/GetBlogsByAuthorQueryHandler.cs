using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Waje.Api.Data.UnitOfWork;
using Waji.Api.CQRS.Queries;
using Waji.Api.Data.Models;
using Waji.Api.Shared.Response;

namespace Waji.Api.CQRS.Handlers.QueryHandlers
{
    public class GetBlogsByAuthorQueryHandler : IRequestHandler<GetBlogsByAuthorQuery, BaseResponse<List<BlogResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetBlogsByAuthorQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<BaseResponse<List<BlogResponse>>> Handle(GetBlogsByAuthorQuery request, CancellationToken cancellationToken)
        {
            var blog = await _unitOfWork.GetRepository<Blog>().GetByIdAsync(p => p.AuthorId == request.AuthorId, false);
            if(!blog.Any())
            {
                return new BaseResponse<List<BlogResponse>>
                {
                    Success = false,
                    Message = $"No Author found for the specified Author ID {request.AuthorId}",
                    StatusCode = HttpStatusCode.NotFound
                };
            };

            var response = blog.Select(b => new BlogResponse()
            {
                Id = b.Id, 
                Url = b.Url,
                Name = b.Name,
            }).ToList();

            return new BaseResponse<List<BlogResponse>>() { Success=true ,Message = "Blog retrieved successfully", Data=response,StatusCode=HttpStatusCode.OK };
        }
    }
}
