using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries;

namespace Application.GenreOperations.Queries;

public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(null)]
    [InlineData(-1)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldReturnError(int? genreId)
    {
        // Arrange
        GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
        query.GenreId = genreId;

        // Act
        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        var result = validator.Validate(query);

        // Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    public void WhenGivenParametersAreValid_Validator_ShouldNotReturnErrors(int bookId)
    {
        // Arrange
        GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
        query.GenreId = bookId;

        // Act
        GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
        var result = validator.Validate(query);

        // Assertion
        result.Errors.Count.Should().Be(0);
    }
}
