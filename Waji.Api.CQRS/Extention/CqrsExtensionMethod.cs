using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Waji.Api.CQRS.Commands;
using Waji.Api.CQRS.Handlers.CommandHandlers;
using Waji.Api.CQRS.Handlers.QueryHandlers;
using Waji.Api.CQRS.Queries;
using Waji.Api.Shared.Response;
using Waji.Api.Shared.Validation;

namespace Waji.Api.CQRS.Extention
{
    public static class CqrsExtensionMethod
    {

        public static void ConfigureCqrsServices(this IServiceCollection services) {

            services.AddScoped<IRequestHandler<CreateBlogCommand, BaseResponse<BlogCreationReponse>>, CreateBlogCommandHandler>();
            services.AddScoped<IRequestHandler<RegisterAuthorCommand, BaseResponse<AuthorResponse>>, RegisterAuthorCommandHandler>();

            services.AddScoped<IRequestHandler<CreatePostCommand, BaseResponse<PostCreationResponse>>, CreatePostCommandHandler>();
            services.AddScoped<IRequestHandler<GetBlogPostsQuery, BaseResponse<List<PostResponse>>>, GetBlogPostsQueryHandler>();
            services.AddScoped<IRequestHandler<GetBlogsByAuthorQuery, BaseResponse<List<BlogResponse>>>, GetBlogsByAuthorQueryHandler>();
           

        }
    }
}
