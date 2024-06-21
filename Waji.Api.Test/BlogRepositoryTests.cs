using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Waje.Api.Data.Repositories;
using Waje.Api.Data.UnitOfWork;
using Waji.Api.Data.Models;
using Waji.Api.Shared.Request;

namespace Waji.Api.Test
{
    public class BlogRepositoryTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IHttpContextAccessor> _IhttpContextAccessor;
        private readonly DbContextOptions<WajeInterViewDbContext> _dbContextOptions;



        public BlogRepositoryTests()
        {


            _unitOfWork = new Mock<IUnitOfWork>();
            _IhttpContextAccessor = new Mock<IHttpContextAccessor>();

            _dbContextOptions = new DbContextOptionsBuilder<WajeInterViewDbContext>()
                .UseInMemoryDatabase(databaseName: "WajeTestDb")
                .Options;
        }



        [Fact]

        public async Task BlogCreation_ShouldReturnSuccess_WhenBlogIsCreated()
        {
            //Arrange
            using var context = new WajeInterViewDbContext(_dbContextOptions);
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Scheme = "http";
            httpContext.Request.Host = new HostString("localhost");

            _IhttpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

            var blogRepository = new BlogRespository(_unitOfWork.Object, context, _IhttpContextAccessor.Object);

            var blogCreationRequest = new BlogCreationRequest
            {
                BlogName = "Test Blog",
                AuthorId = 1
            };

            _unitOfWork.Setup(u => u.GetRepository<Blog>().Create(It.IsAny<Blog>()))
                .Callback<Blog>(b => context.Blogs.Add(b));

            //Act 

            var response = await blogRepository.BlogCreation(blogCreationRequest);
            //Assert

            Assert.True(response.Success);
            Assert.Equal("Blog created successfully", response.Message);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Test Blog", response.Data.BlogName);
            Assert.Equal("http://localhost/test-blog", response.Data.Url);

        }


        [Fact]
        public async Task GetPostsByBlogAsync_ShouldReturnNotFound_WhenNoPostsExist()
        {
            //Arrange

            using var context = new WajeInterViewDbContext(_dbContextOptions);
            var blogRepository = new BlogRespository(_unitOfWork.Object, context, _IhttpContextAccessor.Object);

            //Act 
            var response = await blogRepository.GetPostsByBlogAsync(5);

            //Assert
            Assert.False(response.Success);
            Assert.Equal("No Posts found for the specified blog ID", response.Message);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
