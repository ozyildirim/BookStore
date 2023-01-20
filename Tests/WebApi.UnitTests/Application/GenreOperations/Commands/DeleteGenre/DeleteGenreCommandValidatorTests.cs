using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands;

namespace Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(null)]
    public void WhenInvalidGenreIdIsGiven_Validator_ShouldReturnError(int id)
    {
        // Arrange
        DeleteGenreCommand command = new DeleteGenreCommand(null);
        command.GenreId = id;

        // Act
        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var result = validator.Validate(command);

        // Assertion
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(0)]
    public void WhenValidGenreIdIsGiven_Validator_ShouldNotReturnError(int id)
    {
        // Arrange
        DeleteGenreCommand command = new DeleteGenreCommand(null);
        command.GenreId = id;

        // Act
        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var result = validator.Validate(command);

        // Assertion
        result.Errors.Count.Should().Be(0);
    }
}
