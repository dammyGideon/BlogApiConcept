using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waji.Api.Shared.Response;

namespace Waji.Api.CQRS.Queries
{
    public class GetBlogsByAuthorQuery : IRequest<BaseResponse<List<BlogResponse>>>
    {
        public int AuthorId {  get; set; }

        public GetBlogsByAuthorQuery(int authorId)
        {
            AuthorId = authorId;
        }
    }
}
