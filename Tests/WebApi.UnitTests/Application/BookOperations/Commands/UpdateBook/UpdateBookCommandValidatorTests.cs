using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands;

namespace Application.BookOperations.Commands;

public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(null, null)]
    [InlineData("", 1)]
    [InlineData("", 2)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(string? title, int? genreId)
    {
        // Arrange
        var book = new UpdateBookModel { Title = title, GenreId = genreId };
        UpdateBookCommand command = new UpdateBookCommand(null, null);
        command.Model = book;
        command.BookId = 1;

        // Act
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);

        // Assertion
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        // Arrange
        var book = new UpdateBookModel { Title = "Test Book", GenreId = 1 };
        UpdateBookCommand command = new UpdateBookCommand(null, null);
        command.Model = book;
        command.BookId = 1;

        // Act
        UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);

        // Assertion
        result.Errors.Count.Should().Be(0);
    }
}
