using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands;
using WebApi.DBOperations;
using WebApi.Models.Entities;

namespace Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenExistingGenreIsGiven_InvalidOperationException_ShouldBeReturned()
    {
        // Arrange
        var genre = new Genre { IsActive = true, Name = "Test Genre" };
        _context.Genres.Add(genre);
        _context.SaveChanges();
        CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
        command.Model = new CreateGenreModel { Name = genre.Name, isActive = genre.IsActive };

        // Act
        FluentActions
            .Invoking(() => command.Handle())
            .Should()
            .Throw<InvalidOperationException>()
            .And.Message.Should()
            .Be("Genre already exists!");
    }
}
