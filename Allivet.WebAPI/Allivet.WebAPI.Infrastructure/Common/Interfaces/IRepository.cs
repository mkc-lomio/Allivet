using Allivet.WebAPI.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Allivet.WebAPI.Infrastructure.Common.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Update(TEntity entity, params Expression<Func<TEntity, object>>[] updatedProperties);
        TEntity GetByFilter(Expression<Func<TEntity, bool>> filter);
        void Delete(TEntity entity);
        IEnumerable<TEntity> GetAllByFilter(Expression<Func<TEntity, bool>> filter);
        void UpdateRange(IEnumerable<TEntity> entities);
        void CreateRange(IEnumerable<TEntity> entities);
    }
}
