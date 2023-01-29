using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands;
using WebApi.DBOperations;

namespace Application.GenreOperations.Commands;

public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public DeleteGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenGenreIdNotFoundInDatabase_InvalidOperationException_ShouldBeReturned()
    {
        // Arrange
        var genreId = Int32.MaxValue;

        DeleteGenreCommand command = new DeleteGenreCommand(_context);
        command.GenreId = genreId;

        // Act & Assertion
        FluentActions
            .Invoking(() => command.Handle())
            .Should()
            .Throw<InvalidOperationException>()
            .And.Message.Should()
            .Be("Genre doesn't exists!");
    }

    [Fact]
    public void WhenValidGenreIdIsGiven_Genre_ShouldBeDeleted()
    {
        // Arrange
        var genreId = _context.Genres.First().Id;

        DeleteGenreCommand command = new DeleteGenreCommand(_context);
        command.GenreId = genreId;

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assertion
        var genre = _context.Genres.SingleOrDefault(x => x.Id == genreId);
        genre.Should().BeNull();
    }
}
