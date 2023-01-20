using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands;
using WebApi.DBOperations;

namespace Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public UpdateBookCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenBookIdDoesNotExistInDatabase_InvalidOperationException_ShouldBeReturned()
    {
        // Arrange

        UpdateBookCommand command = new UpdateBookCommand(null, null);
        command.BookId = Int32.MaxValue;

        // Act & Assertion
        FluentActions
            .Invoking(() => command.Handle())
            .Should()
            .Throw<InvalidOperationException>()
            .And.Message.Should()
            .Be("Book not found!");
    }

    [Fact]
    public void WhenValidIdAndModelIsGiven_Book_ShouldBeUpdated()
    {
        // Arrange
        var book = new UpdateBookModel { Title = "Test Book", GenreId = 1 };
        UpdateBookCommand command = new UpdateBookCommand(_context, _mapper);
        command.Model = book;
        command.BookId = 1;

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assertion
        var updatedBook = _context.Books.SingleOrDefault(x => x.Id == 1);
        updatedBook.Should().NotBeNull();
        updatedBook.Title.Should().Be("Test Book");
        updatedBook.GenreId.Should().Be(1);
    }
}
