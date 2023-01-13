using FluentValidation;

namespace WebApi.Application.BookOperations.Commands;

class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(command => command.Model.GenreId)
            .GreaterThan(0)
            .WithMessage("Genre ID must be greater than 0!");
        RuleFor(command => command.Model.PageCount)
            .GreaterThan(0)
            .WithMessage("Page count must be greater than 0!");
        RuleFor(command => command.Model.PublishedDate.Date)
            .NotEmpty()
            .LessThan(DateTime.Now.Date)
            .WithMessage("Published date cannot be equal to current date!");
        RuleFor(command => command.Model.Title)
            .MinimumLength(4)
            .WithMessage("Title length must be greater than 4!");
    }
}
