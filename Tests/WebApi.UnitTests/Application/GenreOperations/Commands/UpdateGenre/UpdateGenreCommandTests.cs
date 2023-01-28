using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands;
using WebApi.DBOperations;

namespace Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public UpdateGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenGivenGenreIdDoesNotExistInDatabase_InvalidOperationException_ShouldBeReturned()
    {
        // Arrange
        UpdateGenreCommand command = new UpdateGenreCommand(null, null);
        command.GenreId = Int32.MaxValue;

        // Act & Assert
        FluentActions.Invoking(() => command.Handle()).Should().Throw<NullReferenceException>();
    }
}
