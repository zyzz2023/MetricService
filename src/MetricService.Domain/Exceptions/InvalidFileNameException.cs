using MetricService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Domain.Exceptions;

public class InvalidFileNameException : DomainException
{
    public InvalidFileNameException(string message) : base("INVALID_FILE_NAME", message) { }
}
