using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands;
using WebApi.DBOperations;
using WebApi.Models.Entities;

namespace Application.BookOperations.Commands;

public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;

    public DeleteBookCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenGivenBookIdDoesNotExist_InvalidOperationException_ShouldReturnError()
    {
        // Arrange

        var lastId = Int32.MaxValue;

        // var book = new Book
        // {
        //     Id = testBook.Id + 1,
        //     Title = "Test Book",
        //     PageCount = 100,
        //     AuthorId = 1,
        //     GenreId = 1,
        //     PublishedDate = DateTime.Today.AddYears(-1).Date
        // };
        DeleteBookCommand command = new DeleteBookCommand(_context);
        command.BookId = lastId;

        // Act & Assertion
        FluentActions
            .Invoking(() => command.Handle())
            .Should()
            .Throw<InvalidOperationException>()
            .And.Message.Should()
            .Be("Book not found!");
    }

    [Fact]
    public void WhenValidIdGiven_Book_ShouldBeDeleted()
    {
        // Arrange
        DeleteBookCommand command = new DeleteBookCommand(_context);
        command.BookId = 1;

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();
        var deletedBook = _context.Books.SingleOrDefault(x => x.Id == command.BookId);
        deletedBook.Should().BeNull();
    }
}
