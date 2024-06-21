using Microsoft.EntityFrameworkCore;
using Moq;
using System.Net;
using System.Threading.Tasks;
using Waje.Api.Data.Repositories;
using Waje.Api.Data.UnitOfWork;
using Waji.Api.Data.Models;
using Waji.Api.Shared.Request;
using Waji.Api.Shared.Response;
using Xunit;

namespace Waji.Api.Test
{
    public class AuthorRepositoryTest
    {
        private readonly Mock<IUnitOfWork> _iUnitOfWork;
        private readonly DbContextOptions<WajeInterViewDbContext> _dbContextOptions;

        public AuthorRepositoryTest()
        {
            _iUnitOfWork = new Mock<IUnitOfWork>();
            _dbContextOptions = new DbContextOptionsBuilder<WajeInterViewDbContext>()
                .UseInMemoryDatabase(databaseName: "WajeTestDb").Options;
        }

        [Fact]
        public async Task AuthorCreation_ShouldReturnSuccess_WhenAuthorIsCreated()
        {
            // Arrange
            using var context = new WajeInterViewDbContext(_dbContextOptions);
            var authorRepository = new AuthorRepository(_iUnitOfWork.Object, context);

            var authorRequest = new CreateAuthorRequest
            {
                Name = "Tunji Micheal",
                Email = "TM@yahoo.com"
            };
            _iUnitOfWork.Setup(u => u.GetRepository<Author>().Create(It.IsAny<Author>()))
                .Callback<Author>(u => context.Authors.Add(u));

            // Act 
            var response = await authorRepository.CreateBlogAuthor(authorRequest);

            // Assert 
            Assert.True(response.Success);
            Assert.Equal("Author Created Successfully", response.Message);
            Assert.Equal("Tunji Micheal", response.Data.Name);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task AuthorName_ShouldReturnFalse_IfAuthorNameExists()
        {
            // Arrange
            using var context = new WajeInterViewDbContext(_dbContextOptions);
            var authorRepository = new AuthorRepository(_iUnitOfWork.Object, context);

            var existingUser = new Author
            {
                Name = "Tunji Micheal",
                Email = "TM@yahoo.com"
            };
            context.Authors.Add(existingUser);
            context.SaveChanges();

            var newAuthorRequest = new CreateAuthorRequest
            {
                Name = "Tunji Micheal",
                Email = "TM@yahoo.com"
            };

            // Act
            var response = await authorRepository.CreateBlogAuthor(newAuthorRequest);

            // Assert 
            Assert.False(response.Success);
            Assert.Equal("This Author Exist", response.Message);
            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        }

        
    }
}
