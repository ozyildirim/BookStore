using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands;

namespace Application.AuthorOperations.Commands;

public class DeleteAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(null)]
    [InlineData(-1)]
    [InlineData(-2)]
    public void WhenInvalidInputIsGiven_Validator_ShouldReturnError(int? authorId)
    {
        // Arrange
        DeleteAuthorCommand command = new DeleteAuthorCommand(null);
        command.AuthorId = authorId;

        // Act
        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}
