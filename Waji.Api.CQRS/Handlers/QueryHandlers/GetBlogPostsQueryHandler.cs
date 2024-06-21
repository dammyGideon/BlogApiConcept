using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waje.Api.Data.UnitOfWork;
using Waji.Api.CQRS.Queries;
using Waji.Api.Data.Models;
using Waji.Api.Shared.Response;

namespace Waji.Api.CQRS.Handlers.QueryHandlers
{
    public class GetBlogPostsQueryHandler : IRequestHandler<GetBlogPostsQuery, BaseResponse<List<PostResponse>>>
    {

        private readonly IUnitOfWork _unitOfWork;
        public GetBlogPostsQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<BaseResponse<List<PostResponse>>> Handle(GetBlogPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _unitOfWork.GetRepository<Post>().GetByIdAsync(p => p.BlogId == request.BlogId, false);

            if (!posts.Any())
            {
                return new BaseResponse<List<PostResponse>>
                {
                    Success = false,
                    Message = "No posts found for the specified blog ID",
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }


            var response = posts.Select(p => new PostResponse
            {
                Id = p.Id,
                BlogId = p.BlogId,
                Title = p.Title,
                Content = p.Content,
                DatePublished = p.DatePublished
            }).ToList();


            return new BaseResponse<List<PostResponse>>
            {
                Success = true,
                Message = "Posts retrieved successfully",
                Data = response,
                StatusCode = System.Net.HttpStatusCode.OK
            };

        }
    }
}
