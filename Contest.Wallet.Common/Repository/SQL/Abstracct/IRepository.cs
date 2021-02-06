using Contest.Wallet.Common.Repository.Helpers;
using Contest.Wallet.Common.Repository.SQL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contest.Wallet.Common.Repository.SQL.Abstract
{
    public interface IRepository<TEntity, TEntityKey> :
        IDisposable where TEntity : EntityBase<TEntityKey>
    {
        Task<IEnumerable<TEntity>> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<int> Count();
        Task<int> Count(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetByIds(List<TEntityKey> ids, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IEnumerable<TEntity>> GetByIds(List<TEntityKey> ids);
        Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IEnumerable<TEntity>> FindBy(Expression<Func<TEntity, bool>> predicate);
        Task DeleteWhere(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> Add(TEntity entity, bool save = true);
        Task<IList<TEntity>> AddRange(IEnumerable<TEntity> entities, bool save = true);
        Task UpdateRange(IEnumerable<TEntity> entities, bool save = true);
        Task DeleteRange(IEnumerable<TEntity> entities, bool save = true);
        Task<TEntity> Get(TEntityKey id);
        Task<TEntity> GetById(TEntityKey id);
        Task<IEnumerable<TEntity>> GetAll();
        Task Update(TEntity entity, bool save = true);
        Task Delete(TEntityKey id, bool save = true);
        Task Delete(TEntity entity, bool save = true);
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        Task<PaginatedList<TEntity>> GetAll(int pageIndex, int pageSize, TEntityKey id);
        Task<PaginatedList<TEntity>> GetAll(int pageIndex, int pageSize, Expression<Func<TEntity, long>> keySelector, OrderBy orderBy = OrderBy.Ascending);
        Task<PaginatedList<TEntity>> GetAll(int pageIndex, int pageSize, Expression<Func<TEntity, long>> keySelector, Expression<Func<TEntity, bool>> predicate, OrderBy orderBy, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
    }
}
