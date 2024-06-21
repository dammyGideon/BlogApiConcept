using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Waje.Api.Data.Contract;
using Waji.Api.CQRS.Commands;
using Waji.Api.Shared.Request;

namespace Waji.Api.Controllers
{
    
    public class BlogController : BaseController
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMediator _mediator;
        public BlogController(IBlogRepository blogRepository, IMediator mediator) {
            _blogRepository = blogRepository;
            _mediator = mediator;
            
        }


        [HttpPost("create-blog")]
        public async Task<IActionResult> CreateNewBlog(BlogCreationRequest request)
        {
            var response = await _blogRepository.BlogCreation(request);
            return StatusCode((int)response.StatusCode,response);
        }

        [HttpPost("create-blog-cqrs")]

        public async Task<IActionResult> CreateNewBlogUsingCQRS(CreateBlogCommand request)
        {
            var response = await _mediator.Send(request);
            return StatusCode((int)response.StatusCode, response);
        }

    }
}
