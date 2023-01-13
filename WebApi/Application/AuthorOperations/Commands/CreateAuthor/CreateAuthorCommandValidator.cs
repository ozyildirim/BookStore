using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands;

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(command => command.Model.Name)
            .MinimumLength(4)
            .WithMessage("Name must be more than 4 characters!");
        RuleFor(command => command.Model.Surname)
            .MinimumLength(4)
            .WithMessage("Surname must be more than 4 characters!");
        RuleFor(command => command.Model.Birthdate)
            .NotEmpty()
            .WithMessage("Birthdate cannot be empty!");
    }
}
