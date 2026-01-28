namespace PanKunik.Results.Tests;

public class ErrorExtensionsTests
{
    [Theory]
    [MemberData(nameof(ErrorTestCases))]
    public void ToFailure_ForError_ShouldReturnFailureWithError(Error error)
    {
        // Act
        var result = error.ToFailure<string>();
        
        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsFailure);
        Assert.Equal(error, result.Error);
    }
    
    public static  IEnumerable<object[]> ErrorTestCases =
    [
        [Error.Validation("VALIDATION", "VALIDATION MESSAGE")],
        [Error.Failure("FAILURE", "FAILURE MESSAGE")],
        [Error.NotFound("NOT_FOUND", "NOT FOUND MESSAGE")]
    ];
}