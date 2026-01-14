using PanKunik.Results.Enums;

namespace PanKunik.Results.Tests;

public class ErrorTests
{
    [Theory]
    [MemberData(nameof(CreateErrorTests))]
    public void Failure_ShouldCreateFailureError(string code, string message)
    {
        // Act
        var error = Error.Failure(code, message);
        
        // Assert
        Assert.NotNull(error);
        Assert.Equal(code, error.Code);
        Assert.Equal(message, error.Message);
        Assert.Equal(ErrorType.Failure, error.Type);
    }
    
    [Theory]
    [MemberData(nameof(CreateErrorTests))]
    public void Validation_ShouldCreateFailureError(string code, string message)
    {
        // Act
        var error = Error.Validation(code, message);
        
        // Assert
        Assert.NotNull(error);
        Assert.Equal(code, error.Code);
        Assert.Equal(message, error.Message);
        Assert.Equal(ErrorType.Validation, error.Type);
    }
    
    [Theory]
    [MemberData(nameof(CreateErrorTests))]
    public void NotFound_ShouldCreateFailureError(string code, string message)
    {
        // Act
        var error = Error.NotFound(code, message);
        
        // Assert
        Assert.NotNull(error);
        Assert.Equal(code, error.Code);
        Assert.Equal(message, error.Message);
        Assert.Equal(ErrorType.NotFound, error.Type);
    }

    public static IEnumerable<object[]> CreateErrorTests =
    [
        ["ERR_CODE", "ERR_MESSAGE"],
        ["", ""]
    ];
}