using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waji.Api.Shared.Response;

namespace Waji.Api.CQRS.Commands
{
    public class CreateBlogCommand : IRequest<BaseResponse<BlogCreationReponse>>
    {
        public string BlogName { get; set; }
        public int AuthorId { get; set; }
    }

   
}
