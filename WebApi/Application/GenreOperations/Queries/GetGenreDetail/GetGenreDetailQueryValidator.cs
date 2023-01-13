using FluentValidation;
using WebApi.Application.GenreOperations.Queries;

public class GetGenreDetailQueryValidator : AbstractValidator<GetGenreDetailQuery>
{
    public GetGenreDetailQueryValidator()
    {
        RuleFor(command => command.GenreId).NotEmpty().WithMessage("GenreId must be specified!");
    }
}
