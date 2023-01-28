using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries;

namespace Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
{
    [Theory]
    [InlineData(null)]
    [InlineData(-1)]
    public void WhenGivenParametersAreNotValidAndNotExist_Validator_ShouldReturnErrors(int? bookId)
    {
        // Arrange
        GetBookDetailQuery query = new GetBookDetailQuery(null, null);
        query.BookId = bookId;

        // Act
        GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
        var result = validator.Validate(query);

        // Assertion
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    public void WhenGivenParametersAreValidAndExist_Validator_ShouldNotReturnErrors(int bookId)
    {
        // Arrange
        GetBookDetailQuery query = new GetBookDetailQuery(null, null);
        query.BookId = bookId;

        // Act
        GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
        var result = validator.Validate(query);

        // Assertion
        result.Errors.Count.Should().Be(0);
    }
}
