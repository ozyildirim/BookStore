using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands;

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(command => command.Model.Name)
            .NotNull()
            .MinimumLength(4)
            .WithMessage("Name must be more than 4 characters!");
        RuleFor(command => command.Model.Surname)
            .NotNull()
            .MinimumLength(4)
            .WithMessage("Surname must be more than 4 characters!");
        RuleFor(command => command.Model.Birthdate)
            .NotNull()
            .NotEmpty()
            .WithMessage("Birthdate cannot be empty!");
    }
}
