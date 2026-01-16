using MetricService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Domain.Exceptions;

public class InvalidExecutionTimeException : DomainException
{
    public InvalidExecutionTimeException(string message) : base("INVALID_EXECUTION_TIME", message) { }
}
