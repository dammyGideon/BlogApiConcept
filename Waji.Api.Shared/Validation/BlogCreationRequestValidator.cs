using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waji.Api.Shared.Request;

namespace Waji.Api.Shared.Validation
{
    public class BlogCreationRequestValidator : AbstractValidator<BlogCreationRequest>
    {
        public BlogCreationRequestValidator() {
            RuleFor(x => x.AuthorId).NotEmpty().GreaterThan(0).WithMessage("AuthorId is required and must be greater than zero.");
            RuleFor(x => x.BlogName).NotEmpty().WithMessage("BlogName is required.");
        }

    }
    
}
