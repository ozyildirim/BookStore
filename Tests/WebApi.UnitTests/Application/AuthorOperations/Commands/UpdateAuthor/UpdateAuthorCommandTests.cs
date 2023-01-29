using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands;
using WebApi.DBOperations;

namespace Application.AuthorOperations.Commands;

public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdateAuthorCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenValidInputsAndModelAreGiven_Author_ShouldBeUpdated()
    {
        // Arrange
        var author = new UpdateAuthorModel
        {
            Name = "Test",
            Surname = "Author",
            Birthdate = new DateTime(2000, 01, 01)
        };
        UpdateAuthorCommand command = new UpdateAuthorCommand(_dbContext, _mapper);
        command.Model = author;
        command.AuthorId = 1;

        // Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assertion
        var updatedAuthor = _dbContext.Authors.SingleOrDefault(x => x.Id == 1);
        updatedAuthor.Should().NotBeNull();
        updatedAuthor.Name.Should().Be("Test");
        updatedAuthor.Surname.Should().Be("Author");
        updatedAuthor.Birthdate.Should().Be(new DateTime(2000, 01, 01));
    }

    [Fact]
    public void WhenGivenAuthorIdDoesNotExistInDatabase_InvalidOperationException_ShouldBeReturned()
    {
        // Arrange
        UpdateAuthorCommand command = new UpdateAuthorCommand(_dbContext, _mapper);
        command.AuthorId = Int32.MaxValue;

        // Act & Assertion
        FluentActions
            .Invoking(() => command.Handle())
            .Should()
            .Throw<InvalidOperationException>()
            .WithMessage("Author not found!");
    }
}
