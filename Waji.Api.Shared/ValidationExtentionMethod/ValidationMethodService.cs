using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waji.Api.Shared.Request;
using Waji.Api.Shared.Validation;

namespace Waji.Api.Shared.ValidationExtentionMethod
{
    public static class ValidationMethodService
    {
        public static void AddValidationServices (this IServiceCollection services)
        {

            services.AddTransient<IValidator<CreateAuthorRequest>, CreateAuthorRequestValidator>();
            services.AddTransient<IValidator<BlogCreationRequest>,BlogCreationRequestValidator>();
            services.AddTransient<IValidator<PostToBlogRequest>, PostToBlogRequestValidator>();
      
        }
    }
}
