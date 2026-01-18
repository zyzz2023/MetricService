using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MetricService.Domain.Interfaces.Common;

public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Универсальный метод для фильтрации, сортировки и пагинации
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="orderBy"></param>
    /// <param name="skip"></param>
    /// <param name="take"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> GetFilteredAsync(
        Expression<Func<TEntity,bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        int? skip = null,
        int? take = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавление сущности в бд
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task AddAsync(TEntity entity);

    /// <summary>
    /// Обновление сущности
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    void Update(TEntity entity);

    /// <summary>
    /// Удаление сущности
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    void Delete(TEntity entity);

    /// <summary>
    /// Проверка наличия сущности в бд
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> IsExsist(TEntity entity);
}
