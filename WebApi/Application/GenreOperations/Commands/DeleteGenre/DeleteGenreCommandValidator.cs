using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands;

public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
{
    public DeleteGenreCommandValidator()
    {
        RuleFor(command => command.GenreId).NotEmpty().WithMessage("GenreId must be specified!");
    }
}
