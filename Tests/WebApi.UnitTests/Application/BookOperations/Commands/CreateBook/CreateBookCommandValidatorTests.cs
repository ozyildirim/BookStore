using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands;

namespace Application.BookOperations.Commands;

public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData("Lord of the Rings", 0, 0, 0)]
    [InlineData("Lord of the Rings", 0, 1, 1)]
    [InlineData("Lord of the Rings", 100, 0, 0)]
    [InlineData("", 0, 0, 0)]
    [InlineData("", 100, 1, 1)]
    [InlineData("", 0, 1, 1)]
    [InlineData("Lor", 100, 1, 1)]
    [InlineData("Lor", 0, 0, 0)]
    [InlineData(" ", 100, 1, 1)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErrors(
        string title,
        int pageCount,
        int genreId,
        int authorId
    )
    {
        //arrange
        CreateBookCommand command = new CreateBookCommand(null, null);
        command.Model = new CreateBookModel
        {
            Title = title,
            PublishedDate = DateTime.Now.AddYears(-1),
            PageCount = pageCount,
            GenreId = genreId,
            AuthorId = authorId,
        };

        //act
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        //assertion
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenDateTimeEqualNowIsGiven_Validator_ShouldReturnError()
    {
        //arrange
        CreateBookCommand command = new CreateBookCommand(null, null);
        command.Model = new CreateBookModel
        {
            Title = "Test Book",
            PublishedDate = DateTime.Now.Date,
            PageCount = 100,
            GenreId = 1,
            AuthorId = 1,
        };

        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
    {
        //arrange
        CreateBookCommand command = new CreateBookCommand(null, null);
        command.Model = new CreateBookModel
        {
            Title = "Test Book",
            PublishedDate = DateTime.Now.AddYears(-1),
            PageCount = 100,
            GenreId = 1,
            AuthorId = 1,
        };

        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}
