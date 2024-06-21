using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Waje.Api.Data.Contract;
using Waje.Api.Data.UnitOfWork;
using Waji.Api.Data.Models;
using Waji.Api.Data.Repositories;
using Waji.Api.Shared.Request;
using Waji.Api.Shared.Response;



namespace Waje.Api.Data.Repositories
{
    public class BlogRespository : GenericRepository<Blog>, IBlogRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;




        public BlogRespository(IUnitOfWork unitOfWork, WajeInterViewDbContext dbContext,IHttpContextAccessor httpContextAccessor): base(dbContext) {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseResponse<BlogCreationReponse>> BlogCreation(BlogCreationRequest request)
        {
            var requestHost = _httpContextAccessor.HttpContext.Request.Host;
            var requestScheme = _httpContextAccessor.HttpContext.Request.Scheme;
            var blogUrl = $"{requestScheme}://{requestHost}/{request.BlogName.ToLower().Replace(' ', '-')}";

            var checkblogExist = await _context.Blogs.FirstOrDefaultAsync(d=>d.Name == request.BlogName);
            if (checkblogExist != null) return new BaseResponse<BlogCreationReponse> { 
                Success = false,
                Message="Blog With this Name Exist",
                StatusCode= HttpStatusCode.Conflict
            }; 
            Blog blogCreation = new()
            {
               AuthorId=request.AuthorId,
               Name = request.BlogName,
               Url = blogUrl
            };
            await _unitOfWork.GetRepository<Blog>().Create(blogCreation);
            await _unitOfWork.SaveChangesAsync();

            BlogCreationReponse response = new()
            {
                BlogName = blogCreation.Name,
                Url =blogCreation.Url,
            };
            return new BaseResponse<BlogCreationReponse>
            {
                Success= true,
                Message= "Blog created successfully",
                Data = response,
                StatusCode =HttpStatusCode.OK,
            };
         
        }

        public async Task<BaseResponse<List<PostResponse>>> GetPostsByBlogAsync(int blogId)
        {
            var post =  _context.Posts.Where(d => d.BlogId == blogId).ToList();
            if (post == null || !post.Any())
                return new BaseResponse<List<PostResponse>>
                {
                    Success = false,
                    Message= "No Posts found for the specified blog ID",
                    StatusCode = HttpStatusCode.NotFound
                };
            var response = post.Select(p=>new PostResponse
            {
                Id = p.Id,
                BlogId = p.BlogId,
                Title = p.Title,
                Content = p.Content,
                DatePublished = p.DatePublished
            }).ToList();


            return new BaseResponse<List<PostResponse>> { Success = true,Message= "Posts retrieved successfully", Data=response,StatusCode= HttpStatusCode.OK};
            
        }
    }
}
