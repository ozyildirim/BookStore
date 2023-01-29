using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands;

namespace Application.GenreOperations.Commands;

public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("123")]
    public void WhenInvalidInputIsGiven_Validator_ShouldReturnErrors(string? name)
    {
        // Arrange
        CreateGenreCommand command = new CreateGenreCommand(null, null);
        command.Model = new CreateGenreModel { Name = name, isActive = true };

        // Act
        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        var result = validator.Validate(command);

        // Assertion
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}
