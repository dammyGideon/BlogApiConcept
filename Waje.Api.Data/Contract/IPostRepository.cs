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
    public interface IPostRepository
    {
        Task<BaseResponse<List<PostResponse>>> GetPostByIdAsync(int postId);
        Task<BaseResponse<PostCreationResponse>> CreatePostToBlog(PostToBlogRequest request);
    }
}
