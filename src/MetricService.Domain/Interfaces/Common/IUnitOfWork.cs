using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Domain.Interfaces.Common
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Инициализация транзакции
        /// </summary>
        /// <returns></returns>
        Task BeginTransactionAsync();

        /// <summary>
        /// Применение измений и подтверждение транзакции
        /// Использование этого метода без BeginTransaction() вызовет исключение!
        /// </summary>
        /// <returns></returns>
        Task CommitTransactionAsync();

        /// <summary>
        /// Откат транзакции и отменение всех изменений в ней
        /// Безопасен для использования (проверяет на null транзакцию)
        /// </summary>
        /// <returns></returns>
        Task RollbackTransactionAsync();
        
        /// <summary>
        /// Используется, если операция с бд всего одна, что бы не открывать транзакцию лишний раз,
        /// так как она откроется автономно при SaveChangesAsync()
        /// </summary>
        /// <returns></returns>
        Task SaveChanesAsync();
    }
}
