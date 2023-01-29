using FluentValidation;
using WebApi.Application.GenreOperations.Queries;

public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
{
    public GetGenreDetailQueryValidator()
    {
        RuleFor(command => command.GenreId).NotNull().WithMessage("GenreId must be specified!");
        RuleFor(command => command.GenreId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("GenreId must be 0 or greater!");
    }
}
