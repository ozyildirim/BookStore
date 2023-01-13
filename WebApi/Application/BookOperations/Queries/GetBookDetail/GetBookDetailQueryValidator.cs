using FluentValidation;

namespace WebApi.Application.BookOperations.Queries;

public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
{
    public GetBookDetailQueryValidator()
    {
        RuleFor(command => command.BookId).NotEmpty().WithMessage("BookId must be specified!");
    }
}
