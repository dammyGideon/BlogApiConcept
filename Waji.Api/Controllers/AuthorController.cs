using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Waje.Api.Data.Contract;
using Waji.Api.CQRS.Commands;
using Waji.Api.CQRS.Queries;
using Waji.Api.Shared.Request;
using Waji.Api.Shared.Response;

namespace Waji.Api.Controllers
{
    public class AuthorController : BaseController
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMediator _mediator;
        public AuthorController(IAuthorRepository authorRepository,  IMediator mediator)
        {
            _authorRepository = authorRepository;
            _mediator = mediator;

        }

        [HttpPost("register-author")]
        public async Task<IActionResult> CreateAuthor(CreateAuthorRequest request)
        {
            var response =await  _authorRepository.CreateBlogAuthor(request);
            return StatusCode((int)response.StatusCode,response);
          
        }


        [HttpPost("register-author-cqrs")]
        public async Task<IActionResult> CreateAuthorcqrs(RegisterAuthorCommand request)
        {
            var response = await _mediator.Send(request);
            return StatusCode((int)response.StatusCode, response);

        }


        [HttpGet("{authorId}/Blog")]
        public async Task<IActionResult> GetBlogByAuthor(int authorId) 
        {
            var response = await _authorRepository.GetBlogsByAuthorAsync(authorId);
            return StatusCode((int) response.StatusCode, response);
        }


        [HttpGet("{authorId}/Blog-cqrs")]
        public async Task<ActionResult<BaseResponse<List<BlogResponse>>>> GetBlogByAuthorId(int authorId)
        {
            var query = new GetBlogsByAuthorQuery(authorId);
            var response = await _mediator.Send(query);
            return StatusCode((int)response.StatusCode, response);
        }



    }
}
