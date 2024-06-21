using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waji.Api.Shared.Response;

namespace Waji.Api.CQRS.Queries
{
    public class GetBlogPostsQuery : IRequest<BaseResponse<List<PostResponse>>>
    {
        public int BlogId { get; set; }

        public GetBlogPostsQuery(int blogId)
        {
            BlogId = blogId;
        }
    }
}
