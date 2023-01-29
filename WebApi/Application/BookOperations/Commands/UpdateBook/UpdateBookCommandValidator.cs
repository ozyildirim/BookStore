using FluentValidation;

namespace WebApi.Application.BookOperations.Commands;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(command => command.Model.GenreId)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0)
            .WithMessage("Genre ID must be greater than 0!");

        RuleFor(command => command.Model.Title)
            .NotEmpty()
            .NotNull()
            .MinimumLength(4)
            .WithMessage("Title length must be greater than 4!");
    }
}
