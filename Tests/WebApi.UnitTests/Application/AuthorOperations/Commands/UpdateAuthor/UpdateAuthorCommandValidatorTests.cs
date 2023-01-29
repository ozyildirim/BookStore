using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands;

namespace Application.AuthorOperations.Commands;

public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(null, null, null)]
    [InlineData("", "", null)]
    [InlineData("asd", "asd", null)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(
        string? name,
        string? surname,
        DateTime? birthdate
    )
    {
        // Arrange
        var author = new UpdateAuthorModel
        {
            Name = name,
            Surname = surname,
            Birthdate = birthdate
        };
        UpdateAuthorCommand command = new UpdateAuthorCommand(null, null);
        command.Model = author;

        // Act
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);

        // Assertion
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}
