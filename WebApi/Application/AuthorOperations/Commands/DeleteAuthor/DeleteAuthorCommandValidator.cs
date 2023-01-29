using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands;

public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
{
    public DeleteAuthorCommandValidator()
    {
        RuleFor(command => command.AuthorId)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .WithMessage("Author Id must be and greater than or equal to 0!");
    }
}
