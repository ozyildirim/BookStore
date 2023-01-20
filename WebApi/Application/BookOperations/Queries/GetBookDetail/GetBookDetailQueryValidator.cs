using FluentValidation;

namespace WebApi.Application.BookOperations.Queries;

public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
{
    public GetBookDetailQueryValidator()
    {
        RuleFor(command => command.BookId).NotNull().WithMessage("BookId must be specified!");
        RuleFor(command => command.BookId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("BookId must be 0 or greater!");
    }
}
