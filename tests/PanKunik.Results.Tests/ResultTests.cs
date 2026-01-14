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
    
    [Theory]
    [InlineData("value")]
    [InlineData(1)]
    [InlineData(true)]
    public void SuccessWithValue_WhenCalledWithValue_ShouldCreateSuccessResultWithExpectedValue(object value)
    {
        // Act
        var result = Result<object>.Success(value);
        
        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(value, result.Value);
        Assert.Null(result.Error);
    }

    [Theory]
    [MemberData(nameof(FailureTestCases))]
    public void FailureOfT_WhenCalled_ShouldCreateFailureResult(Error error)
    {
        // Arrange
        var result = Result<object>.Failure(error);
        
        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Error);
        Assert.Null(result.Value);
        Assert.Equal(error, result.Error);
    }

    public static IEnumerable<object[]> FailureTestCases =
    [
        [Error.Failure("GENERAL", "General error message")],
        [Error.Validation("VALIDATION", "Validation error message")],
        [Error.NotFound("NOT_FOUND", "Not found")]
    ];
}