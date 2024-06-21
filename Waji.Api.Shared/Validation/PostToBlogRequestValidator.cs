using FluentValidation;
using System;
using Waji.Api.Shared.Request;

namespace Waji.Api.Shared.Validation
{
    public class PostToBlogRequestValidator : AbstractValidator<PostToBlogRequest>
    {
        public PostToBlogRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.");

            RuleFor(x => x.BlogId)
                .GreaterThan(0).WithMessage("BlogId must be greater than 0.");



        }
      


    }
}
