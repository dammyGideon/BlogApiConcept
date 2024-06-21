using Moq;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using Waji.Api.CQRS.Commands;
using Waji.Api.CQRS.Handlers.CommandHandlers;
using Waje.Api.Data.UnitOfWork;
using Waji.Api.Data.Models;
using Waji.Api.Shared.Response;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Waji.Api.Test.CQRS.Handlers.CommandHandlers
{
    public class CreateBlogCommandHandlerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

        public CreateBlogCommandHandlerTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        }

     
    }
}
