using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands;

public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
{
    public DeleteGenreCommandValidator()
    {
        RuleFor(command => command.GenreId).NotNull().WithMessage("GenreId can not be null!");
        RuleFor(command => command.GenreId).NotEmpty().WithMessage("GenreId can not be empty!");
        RuleFor(command => command.GenreId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("GenreId can not be empty!");
    }
}
