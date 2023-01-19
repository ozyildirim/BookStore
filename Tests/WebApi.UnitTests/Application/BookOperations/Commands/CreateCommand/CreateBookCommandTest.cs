using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands;
using WebApi.DBOperations;
using WebApi.Models.Entities;

namespace Application.BookOperations.Commands.CreateCommand;

public class CreateBookCommandTest : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateBookCommandTest(CommonTestFixture testFixture, IMapper mapper)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistsBookTitleIsGiven_InvalidOperationException_ShouldBeReturned()
    {
        //arrange
        var book = new Book
        {
            Id = 1,
            Title =
                "Test_WhenAlreadyExistsBookTitleIsGiven_InvalidOperationException_ShouldBeReturned",
            PageCount = 100,
            PublishedDate = new DateTime(1990, 01, 10),
            GenreId = 1,
        };
        _context.Books.Add(book);
        _context.SaveChanges();
        CreateBookCommand command = new CreateBookCommand(_context, _mapper);
        command.Model = new CreateBookModel { Title = book.Title };

        //act
        FluentActions
            .Invoking(() => command.Handle())
            .Should()
            .Throw<InvalidOperationException>()
            .And.Message.Should()
            .Be("Book already exists!");

        //assertion
    }
}
