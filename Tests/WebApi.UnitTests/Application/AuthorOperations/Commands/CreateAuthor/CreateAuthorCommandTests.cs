using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands;
using WebApi.DBOperations;
using WebApi.Models.Entities;

namespace Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateAuthorCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenExistingAuthorIsAdded_InvalidOperationException_ShouldBeReturned()
    {
        // Arrange

        var author = new Author
        {
            Name = "Test",
            Surname = "Author",
            Birthdate = new DateTime(1998, 11, 25).Date,
        };
        _dbContext.Add(author);
        _dbContext.SaveChanges();
        CreateAuthorCommand command = new CreateAuthorCommand(_dbContext, _mapper);
        command.Model = new CreateAuthorModel
        {
            Name = author.Name,
            Surname = author.Surname,
            Birthdate = author.Birthdate
        };

        // Act && Assert
        FluentActions
            .Invoking(() => command.Handle())
            .Should()
            .Throw<InvalidOperationException>()
            .And.Message.Should()
            .Be("Author already exists!");
    }
}
