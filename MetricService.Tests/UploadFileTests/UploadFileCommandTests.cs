using ErrorOr;
using FluentValidation.TestHelper;
using MetricService.Application.Features.Result.Commands;

namespace MetricService.Tests.ValidationTests;

public class UploadFileCommandTests
{
    private readonly UploadFileCommandValidator _validator = new();

    [Fact]
    public void Error_When_FilePath_Is_Empty()
    {
        // Arrange
        var command = new UploadFileCommand("");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.FilePath);
    }

    [Fact]
    public void Error_When_FilePath_Is_Not_Csv()
    {
        // Arrange
        var command = new UploadFileCommand(Path.GetTempFileName());

        // Act 
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.FilePath)
            .WithErrorMessage("File must be a CSV file");
    }
}
