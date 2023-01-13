using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands;

public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidator()
    {
        RuleFor(command => command.Model.Name)
            .MinimumLength(4)
            .WithMessage("Genre name must be more than 4 characters!");
        ;
    }
}
