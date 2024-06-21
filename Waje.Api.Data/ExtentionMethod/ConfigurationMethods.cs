using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Waje.Api.Data.Contract;
using Waje.Api.Data.Repositories;
using Waje.Api.Data.UnitOfWork;
using Waji.Api.Data.Models;
using Waji.Api.Data.Repositories;

namespace Waji.Api.Data.ExtentionMethod
{
    public static class ConfigurationMethods
    {
        //this is void because it should be simple
        public static void DatabaseConfigure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<WajeInterViewDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("WajeDatabase"));
            });
        }
        //this is a return type because it will take chains of services 
        public static IServiceCollection ApplicationServices(this IServiceCollection services)
        {
           
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork,UnitOfWorkService>();
            services.AddScoped<IAuthorRepository,AuthorRepository>();
            services.AddScoped<IBlogRepository, BlogRespository>();
            services.AddScoped<IPostRepository, PostRepository>();


         

            return services;
        }
    }
}
