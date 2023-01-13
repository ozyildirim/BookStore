using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands;

public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(command => command.Model.Name)
            .MinimumLength(4)
            .WithMessage("Genre name must be more than 4 characters!");
    }
}
