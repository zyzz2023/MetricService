using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Contracts.Requests;

public record GetFilteredResultsRequest(
    string? fileName,
    DateTime? fromDate = null,
    DateTime? toDate = null,
    int? take = null);
