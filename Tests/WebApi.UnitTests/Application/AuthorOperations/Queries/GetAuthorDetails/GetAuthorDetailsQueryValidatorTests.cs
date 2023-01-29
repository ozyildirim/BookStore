using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Queries;

namespace Application.AuthorOperations.Queries;

public class GetAuthorDetailsQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(null)]
    [InlineData(-1)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldReturnError(int? authorId)
    {
        // Arrange
        GetAuthorDetailQuery query = new GetAuthorDetailQuery(null, null);
        query.AuthorId = authorId;

        // Act
        GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
        var result = validator.Validate(query);

        // Assertion
        result.Errors.Count.Should().BeGreaterThan(0);
    }
}
