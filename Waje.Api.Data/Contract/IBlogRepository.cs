using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waji.Api.Data.Models;
using Waji.Api.Shared.Request;
using Waji.Api.Shared.Response;

namespace Waje.Api.Data.Contract
{
    public interface IBlogRepository : IGenericRepository<Blog>
    {
        Task<BaseResponse<List<PostResponse>>> GetPostsByBlogAsync(int blogId);
        Task<BaseResponse<BlogCreationReponse>>BlogCreation(BlogCreationRequest request);
    }
}
