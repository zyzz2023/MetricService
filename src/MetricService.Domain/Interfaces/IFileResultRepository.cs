using MetricService.Domain.Entities;
using MetricService.Domain.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Domain.Interfaces;

/// <summary>
/// Разделение репозиториев для разных сущностей
/// необходимо для дальнейшего добавления уникальных методов
/// </summary>
public interface IFileResultRepository : IRepository<FileResult>
{
    Task AddWithOverwriteAsync(
        string fileName, 
        FileResult fileResult, 
        CancellationToken cancellationToken
        );
}
