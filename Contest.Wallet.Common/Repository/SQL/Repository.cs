using Consent.Common.Repository.Extensions;
using Consent.Common.Repository.Helpers;
using Consent.Common.Repository.SQL.Abstract;
using Consent.Common.Repository.SQL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Consent.Common.Repository.SQL
{
    public class Repository<TEntity, TEntityKey>
        : IRepository<TEntity, TEntityKey> where TEntity : EntityBase<TEntityKey>
    {
        protected IDbContext _context;
        protected DbSet<TEntity> _dbSet;

        public Repository(IDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        /// <summary>
        /// Adds the new entity.
        /// </summary>
        /// <param name="entity">Entity instance to add.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        public async virtual Task<TEntity> Add(TEntity entity, bool save = true)
        {
            if (entity.Equals(default(TEntity)))
                throw new ArgumentNullException(nameof(entity));

            this._dbSet.Add(entity);

            if (save)
                await this._context.SaveChangesAsync();
            return entity;
        }

        public async virtual Task<TEntity> GetById(TEntityKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async virtual Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async virtual Task<IEnumerable<TEntity>> GetByIds(List<TEntityKey> ids)
        {
            return await Task.FromResult(_dbSet.Where(l => ids.Any(id => id.Equals(l.Id))));
        }

        public async virtual Task<IEnumerable<TEntity>> GetByIds(List<TEntityKey> ids, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await Task.FromResult(query.Where(l => ids.Any(id => id.Equals(l.Id))));
        }

        /// <summary>
        /// Updates the existing entity.
        /// </summary>
        /// <param name="entity">Entity instance to update.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        public async virtual Task Update(TEntity entity, bool save = true)
        {
            if (entity.Equals(default(TEntity)))
                throw new ArgumentNullException(nameof(entity));

            //this._dbSet.Attach(entity);
            this._context.Entry(entity).State = EntityState.Modified;

            if (save)
                await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// Adds the new list of entities.
        /// </summary>
        /// <param name="entities">List of entity instances to add.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        public async virtual Task<IList<TEntity>> AddRange(IEnumerable<TEntity> entities, bool save = true)
        {
            List<TEntity> entitiesAdded = new List<TEntity>();

            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            foreach (var entity in entities)
            {
                this._dbSet.Add(entity);
                entitiesAdded.Add(entity);
            }
            if (save)
            {
                await _context.SaveChangesAsync();
            }

            return entitiesAdded;
        }
        /// <summary>
        /// Updates the existing list of entities.
        /// </summary>
        /// <param name="entities">List of entity instances to update.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        public virtual async Task UpdateRange(IEnumerable<TEntity> entities, bool save = true)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            foreach (var entity in entities)
            {
                await this.Update(entity, false);
            }

            if (save)
                await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the list of entities from the DB set.
        /// </summary>
        /// <param name="entities">List of entity instances to delete.</param>
        /// <param name="save">Value that specifies whether to save entity or not.</param>
        public async virtual Task DeleteRange(IEnumerable<TEntity> entities, bool save = true)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            foreach (var entity in entities)
            {
                await this.Delete(entity, false);
            }

            if (save)
                await this._context.SaveChangesAsync();
        }

        /// <summary>
		/// Deletes the entity corresponding to the entityId fro the DB set.
		/// </summary>
		/// <param name="entityId">EntityId as a primary key.</param>
		/// <param name="save">Value that specifies whether to save entity or not.</param>
		public async virtual Task Delete(TEntityKey id, bool save = true)
        {
            if (id == null)
                throw new ArgumentOutOfRangeException(nameof(id));
            var entity = await this.Get(id);
            await this.Delete(entity, false);

            if (save)
                await this._context.SaveChangesAsync();
        }
        /// <summary>
		/// Deletes the entity from the DB set.
		/// </summary>
		/// <param name="entity">Entity instance to delete.</param>
		/// <param name="save">Value that specifies whether to save entity or not.</param>
		public async virtual Task Delete(TEntity entity, bool save = true)
        {
            if (entity.Equals(default(TEntity)))
                throw new ArgumentNullException(nameof(entity));

            if (this._context.Entry(entity).State == EntityState.Detached)
            {
                this._dbSet.Attach(entity);
            }

            this._dbSet.Remove(entity);

            if (save)
                await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async virtual Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.FromResult(_dbSet.AsNoTracking().Where(predicate));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public async virtual Task<IEnumerable<TEntity>> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await Task.FromResult(query.AsEnumerable());
        }

        public async virtual Task<int> Count()
        {
            return await Task.FromResult(_context.Set<TEntity>().Count());
        }

        /// <summary>
        /// Get the count of Entities
        /// </summary>
        /// <param name="predicate">predicate condition</param>
        /// <returns>count</returns>
        public async virtual Task<int> Count(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.FromResult(_context.Set<TEntity>().Where(predicate).Count());
        }
        public async virtual Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.FromResult(_context.Set<TEntity>().FirstOrDefault(predicate));
        }

        /// <summary>
		/// Gets the entity corresponding to the entityId.
		/// </summary>
		/// <param name="entityId">EntityId as a primary key.</param>
		/// <returns>Returns the entity corresponding to the entityId.</returns>
		public async virtual Task<TEntity> Get(TEntityKey id)
        {
            if (id == null)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            return await this._dbSet.FindAsync(id);
        }

        /// <summary>
        ///  Gets the entity corresponding to the predicate.
        /// </summary>
        /// <param name="predicate">predicate value</param>
        /// <param name="includeProperties">Inluding entities</param>
        /// <returns>Returns the entity corresponding to the search.</returns>
        public async virtual Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {

            IQueryable<TEntity> query = _context.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await Task.FromResult(query.Where(predicate).FirstOrDefault());
        }

        public async virtual Task<IEnumerable<TEntity>> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.FromResult(_context.Set<TEntity>().Where(predicate));
        }

        public async virtual Task DeleteWhere(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> entities = _context.Set<TEntity>().Where(predicate);

            foreach (var entity in entities)
            {
                _context.Entry<TEntity>(entity).State = EntityState.Deleted;
            }
            await _context.SaveChangesAsync();
        }

        #region paging
        public async Task<PaginatedList<TEntity>> GetAll(int pageIndex, int pageSize, TEntityKey id)
        {
            return await GetAll(pageIndex, pageSize, id);
        }

        public async Task<PaginatedList<TEntity>> GetAll(int pageIndex, int pageSize, Expression<Func<TEntity, long>> keySelector, OrderBy orderBy = OrderBy.Ascending)
        {
            return await GetAll(pageIndex, pageSize, keySelector, null, orderBy);
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = IncludeProperties(includeProperties);
            entities = (predicate != null) ? entities.Where(predicate) : entities;
            return await Task.FromResult(entities.ToList());
        }
        public async Task<PaginatedList<TEntity>> GetAll(int pageIndex, int pageSize, Expression<Func<TEntity, long>> keySelector, Expression<Func<TEntity, bool>> predicate, OrderBy orderBy, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = FilterQuery(keySelector, predicate, orderBy, includeProperties);
            var total = entities.Count();// entities.Count() is different than pageSize
            entities = entities.Paginate(pageIndex, pageSize);
            return await Task.FromResult(entities.ToPaginatedList(pageIndex, pageSize, total));
        }

        public async Task<List<TEntity>> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = IncludeProperties(includeProperties);
            return await Task.FromResult(entities.ToList());
        }
        #endregion

        #region private
        private IQueryable<TEntity> FilterQuery(Expression<Func<TEntity, long>> keySelector, Expression<Func<TEntity, bool>> predicate,
            OrderBy orderBy,
          Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = IncludeProperties(includeProperties);
            entities = (predicate != null) ? entities.Where(predicate) : entities;
            entities = (orderBy == OrderBy.Ascending)
                ? entities.OrderBy(keySelector)
                : entities.OrderByDescending(keySelector);
            return entities;
        }

        private IQueryable<TEntity> IncludeProperties(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> entities = _dbSet;
            foreach (var includeProperty in includeProperties)
            {
                entities = entities.Include(includeProperty);
            }
            return entities;
        }
        #endregion

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
