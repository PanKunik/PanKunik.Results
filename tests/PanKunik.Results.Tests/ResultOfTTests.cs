namespace PanKunik.Results.Tests;

public class ResultOfTTests
{
    
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
        Assert.Equal(error, result.Error);
    }

    public static IEnumerable<object[]> FailureTestCases =
    [
        [Error.Failure("GENERAL", "General error message")],
        [Error.Validation("VALIDATION", "Validation error message")],
        [Error.NotFound("NOT_FOUND", "Not found")]
    ];
    
    [Fact]
    public void Value_WhenFailure_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var result = Result<string>.Failure(Error.Failure("CODE", "MESSAGE"));
        
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => result.Value);
    }
    
    [Fact]
    public void Value_WithValueTypeDefault_ShouldReturnValue()
    {
        // Arrange
        var result = Result<int>.Success(0);
        
        // Act
        Assert.Equal(0, result.Value);
    }
    
    [Fact]
    public void Match_WhenSuccess_ShouldCallOnSuccess()
    {
        // Arrange
        var result = Result<int>.Success(42);

        // Act
        var value = result.Match(
            onSuccess: v => v * 2,
            onFailure: _ => -1
        );

        // Assert
        Assert.Equal(84, value);
    }
    
    [Fact]
    public void Match_WhenFailure_ShouldCallOnFailure()
    {
        // Arrange
        var error = Error.Failure("CODE", "MESSAGE");
        var result = Result<int>.Failure(error);

        // Act
        var value = result.Match(
            onSuccess: v => v * 2,
            onFailure: e => -1
        );

        // Assert
        Assert.Equal(-1, value);
    }
    
    [Fact]
    public void Map_WhenSuccess_ShouldCallOnSuccess()
    {
        // Arrange
        var result = Result<string>.Success("hello");

        // Act
        #pragma warning disable CS0618 // Ignore obsolete warning for test
        var value = result.Map(
            onSuccess: v => v + " world",
            onFailure: _ => "fail"
        );
        #pragma warning restore CS0618

        // Assert
        Assert.Equal("hello world", value);
    }

    [Fact]
    public void Map_WhenFailure_ShouldCallOnFailure()
    {
        // Arrange
        var error = Error.Failure("CODE", "MESSAGE");
        var result = Result<string>.Failure(error);

        // Act
        #pragma warning disable CS0618 // Ignore obsolete warning for test
        var value = result.Map(
            onSuccess: v => v + " world",
            onFailure: e => "fail"
        );
        #pragma warning restore CS0618

        // Assert
        Assert.Equal("fail", value);
    }
}