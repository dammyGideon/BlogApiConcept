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
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public PostRepository(IUnitOfWork unitOfWork, WajeInterViewDbContext dbContext):base(dbContext) => _unitOfWork = unitOfWork;
        public async Task<BaseResponse<PostCreationResponse>> CreatePostToBlog(PostToBlogRequest request)
        {
            var blogExist = await _context.Blogs.FirstOrDefaultAsync(d=>d.Id == request.BlogId);
            if (blogExist == null) new BaseResponse<PostCreationResponse> {Success=false,Message="No Blog found",StatusCode=HttpStatusCode.NotFound };

            var postExist = await _context.Posts.FirstOrDefaultAsync(d=>d.Title == request.Title);
            if (blogExist == null) new BaseResponse<PostCreationResponse> { Success = false, Message = "This Blog Post Exist", StatusCode = HttpStatusCode.NotFound };

            Post blogPost = new()
            {
                Title = request.Title,
                Content = request.Content,
                DatePublished = request.DatePublished,
                BlogId = request.BlogId,
            };
            await _unitOfWork.GetRepository<Post>().Create(blogPost);
            await _unitOfWork.SaveChangesAsync();


            PostCreationResponse response = new()
            {
                Title = blogPost.Title,
                Content = blogPost.Content,
                DatePublished = blogPost.DatePublished,
                BlogId = blogPost.BlogId,
            };

            return new BaseResponse<PostCreationResponse>
            {
                Success = true,
                Message="Post created Successfully",
                Data = response,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<BaseResponse<List<PostResponse>>> GetPostByIdAsync(int blogId)
        {
            var postExist = await _context.Posts.Where(d=>d.BlogId == blogId).ToListAsync();
            if (postExist == null) new BaseResponse<List<PostResponse>> { Success = false, Message="", StatusCode=HttpStatusCode.NotFound};

            var response = postExist.Select(d => new PostResponse()
            {
                Id = d.Id,
                Title = d.Title,
                Content = d.Content,
                DatePublished = d.DatePublished,
                BlogId = d.BlogId,
            }).ToList();
            return new BaseResponse<List<PostResponse>>
            {
                Success= true,
                Message= "Post retrieved successfully",
                Data = response,
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
