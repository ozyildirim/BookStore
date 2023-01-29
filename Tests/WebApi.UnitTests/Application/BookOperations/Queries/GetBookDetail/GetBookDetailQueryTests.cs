using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries;
using WebApi.DBOperations;
using WebApi.Models.Entities;

namespace Application.BookOperations.Queries;

public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetBookDetailQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenBookIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturned()
    {
        // Arrange
        GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
        query.BookId = -1;

        // Act & Assertion
        FluentActions
            .Invoking(() => query.Handle())
            .Should()
            .Throw<InvalidOperationException>()
            .And.Message.Should()
            .Be("Book not found!");
    }

    [Fact]
    public void WhenValidBookIdGiven_Book_ShouldBeReturned()
    {
        // Arrange
        GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
        query.BookId = 1;

        // Act && Assert
        var book = _context.Books.SingleOrDefault(x => x.Id == query.BookId);
        book.Should().NotBeNull();
        book.Should().BeOfType<Book>();
    }
}
