using MetricService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Domain.Exceptions
{
    public class InvalidFileResultException : DomainException
    {
        public InvalidFileResultException(string message) : base("INVALID_FILE_RESULT", message) { }
    }
}
