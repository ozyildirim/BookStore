using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands;
using WebApi.DBOperations;
using WebApi.Models.Entities;

namespace Application.BookOperations.Commands.CreateBook;

public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    [Fact]
    public void WhenInvalidInputsAreGiven_Validator_ShouldReturnErorrs()
    {
        //arrange
        CreateBookCommand command = new CreateBookCommand(null, null);
        command.Model = new CreateBookModel
        {
            Title = "",
            PublishedDate = DateTime.Now,
            PageCount = 0,
            GenreId = 0,
            AuthorId = 0,
        };

        //act
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        //assertion
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}
