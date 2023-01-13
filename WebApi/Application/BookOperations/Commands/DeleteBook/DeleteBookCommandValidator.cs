using FluentValidation;

namespace WebApi.Application.BookOperations.Commands;
public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(command => command.BookId)
            .GreaterThan(0)
            .WithMessage(errorMessage: "Book Id must be greater than 0!");
    }
}
