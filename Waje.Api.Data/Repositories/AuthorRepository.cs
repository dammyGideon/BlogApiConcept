using Waji.Api.Data.Models;
using Waje.Api.Data.Contract;
using Waji.Api.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Waji.Api.Shared.Response;
using System.Net;
using Waje.Api.Data.UnitOfWork;
using Waji.Api.Shared.Request;

namespace Waje.Api.Data.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        private readonly IUnitOfWork _unitOfWork;


        public AuthorRepository(IUnitOfWork unitOfWork, WajeInterViewDbContext dbContext) : base(dbContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<AuthorResponse>> GetAuthorByIdAsync(int authorId)
        {
            var isUserExist = await _context.Authors.FirstOrDefaultAsync(d => d.Id == authorId);
            if (isUserExist == null) return new BaseResponse<AuthorResponse>
            {
                Message = "Author with this Id Does not Exist",
                StatusCode = HttpStatusCode.NotFound
            };

            AuthorResponse authorResponse = new()
            {
                Id = isUserExist.Id,
                Name = isUserExist.Name,
                Email = isUserExist.Email,
            };
            return new BaseResponse<AuthorResponse> { Message = "Author retrieved Successfully", Data = authorResponse, StatusCode = HttpStatusCode.OK };
        }

        public async Task<BaseResponse<List<BlogResponse>>> GetBlogsByAuthorAsync(int authorId)
        {
            var blog = await _context.Blogs.Where(b => b.AuthorId == authorId).ToListAsync();

            if (blog == null || !blog.Any()) return new BaseResponse<List<BlogResponse>>
            { Success = false, Message = "No blogs found for the specified author ID", StatusCode = HttpStatusCode.NotFound };

            var response = blog.Select(d => new BlogResponse()
            {
                Id = d.Id,
                Name = d.Name,
                Url = d.Url,

            }).ToList();
            return new BaseResponse<List<BlogResponse>>
            {
                Success = true,
                Message = "Blogs retrieved successfully",
                Data = response,
                StatusCode = HttpStatusCode.OK

            };
        }

        public async Task<BaseResponse<AuthorResponse>> CreateBlogAuthor(CreateAuthorRequest request)
        {
            var authorExist = await _context.Authors.FirstOrDefaultAsync(d => d.Email == request.Email);
            if (authorExist != null)
            {
                return new BaseResponse<AuthorResponse>
                {
                    Success = false,
                    Message = "This Author Exist",
                    StatusCode = HttpStatusCode.Conflict
                };
            }
            Author author = new()
            {
                Name = request.Name,
                Email = request.Email,

            };
            await _unitOfWork.GetRepository<Author>().Create(author);
            await _unitOfWork.SaveChangesAsync();

            AuthorResponse response = new()
            {
                Id = author.Id,
                Name = author.Name,
                Email = author.Email,
            };

            return new BaseResponse<AuthorResponse> { Message = "Author Created Successfully", Data = response, StatusCode = HttpStatusCode.OK };
        }
    
    }
}
