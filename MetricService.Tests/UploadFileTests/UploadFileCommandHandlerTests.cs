using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetricService.Application.Features.Result.Commands;
using MetricService.Domain.Entities;
using MetricService.Domain.Interfaces;
using Moq;

namespace MetricService.Tests.UploadFileTests;

public class UploadFileCommandHandlerTests
{
    private readonly Mock<IFileResultRepository> _repositoryMock;
    private readonly UploadFileCommandHandler _handler;

    public UploadFileCommandHandlerTests()
    {
        _repositoryMock = new Mock<IFileResultRepository>();
        _handler = new UploadFileCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Error_For_Negative_Value()
    {
        // Arrange 
        var filePath = TestFilePath("invalid_value.csv");
        var command = new UploadFileCommand(filePath);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsError);

        _repositoryMock.Verify(
            r => r.AddWithOverwriteAsync(
                It.IsAny<string>(),
                It.IsAny<FileResult>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Error_For_Negative_ExecutionTime()
    {
        // Arrange 
        var filePath = TestFilePath("invalid_time_execution.csv");
        var command = new UploadFileCommand(filePath);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsError);

        _repositoryMock.Verify(
            r => r.AddWithOverwriteAsync(
                It.IsAny<string>(),
                It.IsAny<FileResult>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Error_For_Date_Less_Than_2000()
    {
        // Arrange 
        var filePath = TestFilePath("invalid_date_less.csv");
        var command = new UploadFileCommand(filePath);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsError);

        _repositoryMock.Verify(
            r => r.AddWithOverwriteAsync(
                It.IsAny<string>(),
                It.IsAny<FileResult>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Error_For_Date_Over_Than_Now()
    {
        // Arrange 
        var filePath = TestFilePath("invalid_date_over.csv");
        var command = new UploadFileCommand(filePath);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsError);

        _repositoryMock.Verify(
            r => r.AddWithOverwriteAsync(
                It.IsAny<string>(),
                It.IsAny<FileResult>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Error_For_Rows_Less_Than_1()
    {
        // Arrange 
        var filePath = TestFilePath("invalid_less_1.csv");
        var command = new UploadFileCommand(filePath);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsError);

        _repositoryMock.Verify(
            r => r.AddWithOverwriteAsync(
                It.IsAny<string>(),
                It.IsAny<FileResult>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Error_For_Rows_Over_Than_10000()
    {
        // Arrange 
        var filePath = TestFilePath("invalid_over_10000.csv");
        var command = new UploadFileCommand(filePath);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsError);

        _repositoryMock.Verify(
            r => r.AddWithOverwriteAsync(
                It.IsAny<string>(),
                It.IsAny<FileResult>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Error_For_Invalid_Arguments_Type()
    {
        // Arrange 
        var filePath = TestFilePath("invalid_type_arguments.csv");
        var command = new UploadFileCommand(filePath);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsError);

        _repositoryMock.Verify(
            r => r.AddWithOverwriteAsync(
                It.IsAny<string>(),
                It.IsAny<FileResult>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Save_File_When_File_Is_Valid_Over_0()
    {
        // Arrange 
        var filePath = TestFilePath("valid_row_1.csv");
        var command = new UploadFileCommand(filePath);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsError);

        _repositoryMock.Verify(
            r => r.AddWithOverwriteAsync(
                It.IsAny<string>(),
                It.IsAny<FileResult>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Save_File_When_File_Is_Valid_Less_10000()
    {
        // Arrange 
        var filePath = TestFilePath("valid_row_10000.csv");
        var command = new UploadFileCommand(filePath);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsError);

        _repositoryMock.Verify(
            r => r.AddWithOverwriteAsync(
                It.IsAny<string>(),
                It.IsAny<FileResult>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    private static string TestFilePath(string fileName)
    {
        return Path.Combine(
            AppContext.BaseDirectory,
            "DataForTests",
            "Csv",
            fileName);
    }
}
