using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries;
using WebApi.DBOperations;
using WebApi.Models.Entities;

namespace Application.GenreOperations.Queries;

public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetGenreDetailQueryTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenGenreIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturned()
    {
        // Arrange
        GetGenreDetailQuery query = new GetGenreDetailQuery(_dbContext, _mapper);
        query.GenreId = -1;

        // Act && Assert
        FluentActions
            .Invoking(() => query.Handle())
            .Should()
            .Throw<InvalidOperationException>()
            .And.Message.Should()
            .Be("Genre not found!");
    }

    [Fact]
    public void WhenValidGenreIdGiven_Genre_ShouldBeReturned()
    {
        // Arrange
        GetGenreDetailQuery query = new GetGenreDetailQuery(_dbContext, _mapper);
        query.GenreId = 1;

        // Act && Assert

        var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == query.GenreId);
        genre.Should().NotBeNull();
        genre.Should().BeOfType<Genre>();
    }
}
