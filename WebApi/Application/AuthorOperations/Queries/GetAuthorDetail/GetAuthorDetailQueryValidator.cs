using FluentValidation;

namespace WebApi.Application.AuthorOperations.Queries;

public class GetAuthorDetailQueryValidator : AbstractValidator<GetAuthorDetailQuery>
{
    public GetAuthorDetailQueryValidator()
    {
        RuleFor(query => query.AuthorId)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .WithMessage("Author Id must be greater than or equal to 0!");
    }
}
