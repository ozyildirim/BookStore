using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands;
using WebApi.DBOperations;

namespace Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _dbContext;

    public DeleteAuthorCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
    }

    [Fact]
    public void WhenGivenAuthorIdDoesNotExist_InvalidOperationException_ShouldReturnError()
    {
        // Arrange

        var lastId = Int32.MaxValue;
        DeleteAuthorCommand command = new DeleteAuthorCommand(_dbContext);
        command.AuthorId = lastId;

        // Act && Assertion
        FluentActions
            .Invoking(() => command.Handle())
            .Should()
            .Throw<InvalidOperationException>()
            .And.Message.Should()
            .Be("Author not found!");
    }
}
