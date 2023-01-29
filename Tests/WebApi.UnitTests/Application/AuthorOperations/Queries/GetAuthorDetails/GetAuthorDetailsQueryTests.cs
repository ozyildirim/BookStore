using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Queries;
using WebApi.DBOperations;
using WebApi.Models.Entities;

namespace Application.AuthorOperations.Queries;

public class GetAuthorDetailsQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAuthorDetailsQueryTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenAuthorIdDoesNotExistInDb_InvalidOperationException_ShouldBeReturned()
    {
        // Arrange
        GetAuthorDetailQuery query = new GetAuthorDetailQuery(_dbContext, _mapper);
        query.AuthorId = -1;

        // Act && Assertion
        FluentActions
            .Invoking(() => query.Handle())
            .Should()
            .Throw<InvalidOperationException>()
            .And.Message.Should()
            .Be("Author not found!");
    }

    [Fact]
    public void WhenValidAuthorIdGiven_Author_ShouldBeReturned()
    {
        // Arrange
        GetAuthorDetailQuery query = new GetAuthorDetailQuery(_dbContext, _mapper);
        query.AuthorId = 1;

        // Act && Assert
        var author = _dbContext.Authors.SingleOrDefault(x => x.Id == query.AuthorId);
        author.Should().NotBeNull();
        author.Should().BeOfType<Author>();
    }
}
