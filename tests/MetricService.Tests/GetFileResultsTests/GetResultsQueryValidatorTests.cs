using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using MetricService.Application.Features.Result.Queries;
using ErrorOr;

namespace MetricService.Tests.GetFileResultsTests;

public class GetResultsQueryValidatorTests
{
    private readonly GetResultsQueryValidator _validator = new();

    [Fact]
    public void Error_For_Invalid_FromDate()
    {
        // Arrange 
        var query = new GetResultsQuery(FromDate: new DateTime(1999, 01, 24));

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FromDate);
    }

    [Fact]
    public void Error_For_Invalid_ToDate()
    {
        // Arrange 
        var query = new GetResultsQuery(ToDate: new DateTime(1999, 01, 23));

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ToDate);
    }

    [Fact]
    public void Error_For_Invalid_FromAverageExecutionTime()
    {
        // Arrange 
        var query = new GetResultsQuery(FromAverageExecutionTime: -1.0);

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FromAverageExecutionTime);
    }

    [Fact]
    public void Error_For_Invalid_ToAverageExecutionTime()
    {
        // Arrange 
        var query = new GetResultsQuery(ToAverageExecutionTime: -1.0);

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ToAverageExecutionTime);
    }

    [Fact]
    public void Error_For_Invalid_FromAverageValue()
    {
        // Arrange 
        var query = new GetResultsQuery(FromAverageValue: -1.0);

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.FromAverageValue);
    }

    [Fact]
    public void Error_For_Invalid_ToAverageValue()
    {
        // Arrange 
        var query = new GetResultsQuery(ToAverageValue: -1.0);

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ToAverageValue);
    }
}
