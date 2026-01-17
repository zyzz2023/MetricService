using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Domain.Interfaces.Common
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Добавление сущности в бд
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(T entity);

        /// <summary>
        /// Обновление сущности
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Update(T entity);

        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Delete(T entity);

        /// <summary>
        /// Проверка наличия сущности в бд
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> IsExsist(T entity);
    }
}
