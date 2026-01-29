namespace PanKunik.Results.Tests;

public class ResultTests
{
    [Fact]
    public void Success_WhenCalled_ShouldCreateSuccessResult()
    {
        // Act
        var result = Result.Success();
        
        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Null(result.Error);
    }

    [Theory]
    [MemberData(nameof(FailureTestCases))]
    public void Failure_WhenCalled_ShouldCreateFailureResult(Error error)
    {
        // Arrange
        var result = Result.Failure(error);
        
        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Error);
        Assert.Equal(error, result.Error);
    }

    public static IEnumerable<object[]> FailureTestCases =
    [
        [Error.Failure("GENERAL", "General error message")],
        [Error.Validation("VALIDATION", "Validation error message")],
        [Error.NotFound("NOT_FOUND", "Not found")]
    ];
    
    [Fact]
    public void Match_WhenSuccess_ShouldCallOnSuccess()
    {
        // Arrange
        var result = Result.Success();
        var called = false;

        // Act
        result.Match(
            onSuccess: () =>
            {
                called = true;
                return called;
            },
            onFailure: _ => false
        );

        // Assert
        Assert.True(called);
    }
    
    [Fact]
    public void Match_WhenFailure_ShouldCallOnFailure()
    {
        // Arrange
        var error = Error.Failure("CODE", "MESSAGE");
        var result = Result.Failure(error);
        var called = false;

        // Act
        result.Match(
            onSuccess: () => false,
            onFailure: _ =>
            {
                called = true;
                return true;
            }
        );

        // Assert
        Assert.True(called);
    }
}