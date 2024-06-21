using MediatR;

using Microsoft.AspNetCore.Mvc;
using Waje.Api.Data.Contract;
using Waji.Api.CQRS.Commands;
using Waji.Api.CQRS.Queries;
using Waji.Api.Shared.Request;
using Waji.Api.Shared.Response;

namespace Waji.Api.Controllers
{
    
    public class PostController : BaseController
    {
        private readonly IPostRepository _postRepository;
        private readonly IMediator _mediator;
        public PostController(IPostRepository postRepository, IMediator mediator)
        {
            _postRepository = postRepository;
            _mediator = mediator;
        }

        [HttpPost("create-post")]
        public async Task<IActionResult> CreatePostToABlog(PostToBlogRequest request)
        {
            var response = await _postRepository.CreatePostToBlog(request);
            return StatusCode((int)response.StatusCode, response);
        }
        [HttpGet("{blogId}/post-by-blog")]
        public async Task<IActionResult> GetAllPostByBlog(int blogId)
        {
            var response = await _postRepository.GetPostByIdAsync(blogId);
            return StatusCode((int)response.StatusCode,response);
        }


        [HttpPost("post-by-blog-cqrs")]
        public async Task<ActionResult<BaseResponse<List<PostResponse>>>> CreatePostsByBlog(CreateBlogCommand command)
        {
            var response = await _mediator.Send(command);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("{blogId}/post-by-blog-cqrs")]
        public async Task<ActionResult<BaseResponse<List<PostResponse>>>> GetPostsByBlog(int blogId)
        {
            var query = new GetBlogPostsQuery(blogId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
