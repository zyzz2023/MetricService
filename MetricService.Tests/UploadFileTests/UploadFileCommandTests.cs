using ErrorOr;
using FluentValidation.TestHelper;
using MetricService.Application.Features.Result.Commands;
using Microsoft.AspNetCore.Http;
using Moq;

namespace MetricService.Tests.ValidationTests;

public class UploadFileCommandTests
{
    private readonly UploadFileCommandValidator _validator = new();

    [Fact]
    public void Error_When_File_Is_Not_Csv()
    {
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.FileName).Returns("test.txt");
        mockFile.Setup(f => f.Length).Returns(10 * 1024); // 10KB
    
        var command = new UploadFileCommand(mockFile.Object);
        var validator = new UploadFileCommandValidator();

        // Act 
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.file)
            .WithErrorMessage("File must be a CSV file");
    }
}
