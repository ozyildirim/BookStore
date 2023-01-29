using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands;

namespace Application.GenreOperations.Commands;

public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(null)]
    [InlineData("a")]
    [InlineData("ab")]
    [InlineData("abc")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string? name)
    {
        // Arrange
        var genre = new UpdateGenreModel { Name = name };
        UpdateGenreCommand command = new UpdateGenreCommand(null, null);
        command.GenreId = 1;
        command.Model = genre;

        // Act
        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(command);

        // Assertion
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}
