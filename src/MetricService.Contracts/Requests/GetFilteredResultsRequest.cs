using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Contracts.Requests;

public record GetFilteredResultsRequest(
    string? FileName,
    DateTime? FromDate = null,
    DateTime? ToDate = null,
    double? FromAverageValue = null,
    double? ToAverageValue = null,
    double? FromAverageExecutionTime = null,
    double? ToAverageExecutionTime = null);
