using Waji.Api.Data.Models;
using Waji.Api.Shared.Request;
using Waji.Api.Shared.Response;

namespace Waje.Api.Data.Contract
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
       Task<BaseResponse<AuthorResponse>> GetAuthorByIdAsync(int authorId);
        Task<BaseResponse<List<BlogResponse>>> GetBlogsByAuthorAsync(int authorId);

        Task<BaseResponse<AuthorResponse>> CreateBlogAuthor(CreateAuthorRequest request);
    }
}
