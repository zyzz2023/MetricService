using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Domain.Common;

public class DomainException : Exception
{
    public string Code { get; } = string.Empty;
    public DomainException(string code, string message) : base(message)
    {
        Code = code;
    }
}
