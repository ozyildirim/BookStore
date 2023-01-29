using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands;
using WebApi.DBOperations;
using WebApi.Models.Entities;

namespace Application.BookOperations.Commands;

public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateBookCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenExistingBookTitleIsGiven_InvalidOperationException_ShouldBeReturned()
    {
        //arrange
        var book = new Book
        {
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

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
    {
        //Arrange
        CreateBookCommand command = new CreateBookCommand(_context, _mapper);
        CreateBookModel model = new CreateBookModel
        {
            Title = "Hobbit",
            PageCount = 100,
            PublishedDate = DateTime.Now.AddYears(-10),
            GenreId = 1,
            AuthorId = 1
        };

        command.Model = model;

        //Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        //Assert
        var book = _context.Books.SingleOrDefault(x => x.Title == model.Title);
        book.Should().NotBeNull();
        book.PageCount.Should().Be(model.PageCount);
        book.PublishedDate.Should().Be(model.PublishedDate);
        book.Title.Should().Be(model.Title);
        book.AuthorId.Should().Be(model.AuthorId);
        book.GenreId.Should().Be(model.GenreId);
    }
}
