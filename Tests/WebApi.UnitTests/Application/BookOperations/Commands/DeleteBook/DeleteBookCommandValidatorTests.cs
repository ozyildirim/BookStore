using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands;
using WebApi.DBOperations;
using WebApi.Models.Entities;

namespace Application.BookOperations.Commands;

public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public DeleteBookCommandValidatorTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Theory]
    [InlineData(null)]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-2)]
    public void WhenGivenBookIdEqualOrLessThanZero_InvalidOperationException_ShouldReturnError(
        int id
    )
    {
        // Arrange

        DeleteBookCommand command = new DeleteBookCommand(null);
        command.BookId = id;

        // Act

        DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
        var result = validator.Validate(command);

        // Assertion
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidIdGiven_Book_ShouldBeDeleted()
    {
        // Arrange
        var lastBookId = _context.Books.Last().Id;

        DeleteBookCommand command = new DeleteBookCommand(_context);

        command.BookId = lastBookId;

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        var deletedBook = _context.Books.SingleOrDefault(x => x.Id == lastBookId);
        deletedBook.Should().BeNull();
    }
}
