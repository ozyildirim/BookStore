using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands;

public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(command => command.Model.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(4)
            .WithMessage("Name must be more than 4 characters!");
        RuleFor(command => command.Model.Surname)
            .NotNull()
            .NotEmpty()
            .MinimumLength(4)
            .WithMessage("Surname must be more than 4 characters!");
        RuleFor(command => command.Model.Birthdate)
            .NotNull()
            .NotEmpty()
            .NotEmpty()
            .WithMessage("Birthdate cannot be empty!");
    }
}
