using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ShipTo.Core.IRepositories._Base;
using ShipTo.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using System.Linq.Dynamic.Core;

namespace ShipTo.Infrastructure.Repositories._Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
       
        private DbSet<T> dbSet;
        private readonly ShipToContext _context;

        public BaseRepository(ShipToContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        public T GetById(object id, Expression<Func<T, object>> includeExpression = null)
        {
            var item = dbSet.Find(id);
            if (includeExpression != null)
            {
                item = dbSet.Find(id);
                if (item == null)
                    return null;

                _context.Entry(item).Reference(includeExpression).Load();
            }
            return item;
        }

        public Task<T> GetByIdAsync(object id, Expression<Func<T, object>> includeExpression = null)
        {
            var item = dbSet.FindAsync(id).Result;
            if (includeExpression != null)
            {
                if (item == null)
                    return null;

                _context.Entry(item).Reference(includeExpression).LoadAsync();
            }
            return new Task<T>(() => item);
        }

        public T Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = GetQueryable(predicate, include);
            return query.FirstOrDefault();
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = GetQueryable(predicate, include);
            return query.FirstOrDefaultAsync();
        }

        public T AsNoTracking(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return GetQueryable(predicate, include).AsNoTracking().FirstOrDefault();
        }

        public Task<T> AsNoTrackingAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return GetQueryable(predicate, include).AsNoTracking().FirstOrDefaultAsync();
        }

        public IQueryable<T> AsNoTrackingMany(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<T>> AsNoTrackingManyAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return new Task<IQueryable<T>>(() => GetQueryable(predicate, include).AsNoTracking());
        }

        /// <summary>
        /// Returns a single instance of T but throws exception if none is found
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T GetSingle(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = GetQueryable(predicate, include);
            return query.SingleOrDefault();
        }

        public Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = GetQueryable(predicate, include);
            return query.SingleOrDefaultAsync();
        }

        public IQueryable<T> GetAll()
        {
            return this.GetAll(null, null, null, null, null);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return this.GetAll(predicate, null, null, null, null);
        }

        public IQueryable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        {
            return this.GetAll(null, include, null, null, null);
        }

        public Task<IQueryable<T>> GetAllAsync()
        {
            return this.GetAllAsync(null, null, null, null, null);
        }

        public Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return this.GetAllAsync(predicate, null, null, null, null);
        }

        public Task<IQueryable<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        {
            return this.GetAllAsync(null, include, null, null, null);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return GetQueryable(predicate, include);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null)
        {
            IQueryable<T> query = GetQueryable(predicate, include);

            if (orderBy != null)
                query = orderBy(query);

            if (skip != null && skip.HasValue)
                query = query.Skip(skip.Value);

            if (take != null && take.HasValue)
                query = query.Take(take.Value);

            return query;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                    string orderBy = null, string orderDirection = "asc", int? skip = null, int? take = null)
        {
            IQueryable<T> query = GetQueryable(predicate, include);

            if (orderBy != null)
                query = query.OrderBy(orderBy, orderDirection);

            if (skip != null && skip.HasValue)
                query = query.Skip(skip.Value);

            if (take != null && take.HasValue)
                query = query.Take(take.Value);

            return query;
        }

        public Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                                 Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null)
        {
            IQueryable<T> query = GetQueryable(predicate, include);

            if (orderBy != null)
                query = orderBy(query);

            if (skip != null && skip.HasValue)
                query = query.Skip(skip.Value);

            if (take != null && take.HasValue)
                query = query.Take(take.Value);

            return new Task<IQueryable<T>>(() => query);
        }

        public Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return new Task<IQueryable<T>>(() => GetQueryable(predicate, include));
        }

        public Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>,
            IIncludableQueryable<T, object>> include = null, string orderBy = null, string orderDirection = "asc", int? skip = null, int? take = null)
        {
            IQueryable<T> query = GetQueryable(predicate, include);

            if (orderBy != null)
                query = query.OrderBy(orderBy, orderDirection);

            if (skip != null && skip.HasValue)
                query = query.Skip(skip.Value);

            if (take != null && take.HasValue)
                query = query.Take(take.Value);

            return new Task<IQueryable<T>>(() => query);
        }

        public int Count()
        {
            return dbSet.Count();
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Count(predicate);
        }

        private IQueryable<T> GetQueryable(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = dbSet;

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            return query;
        }
        /////////////////////////////////////////////// Write functions //////////////////////////////////////////////////////////

        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void AddAsync(T entity)
        {
            dbSet.AddAsync(entity);
        }

        public virtual void Add(List<T> entities)
        {
            dbSet.AddRange(entities);
        }

        public virtual Task AddAsync(List<T> entities)
        {
            return dbSet.AddRangeAsync(entities);
        }

        public virtual void BulkInsert(List<T> entities)
        {
            _context.BulkInsert(entities);
        }

        public virtual Task BulkInsertAsync(List<T> entities)
        {
            return _context.BulkInsertAsync(entities);
        }

        public T Update(T entity)
        {
            entity = dbSet.Update(entity).Entity;
            return entity;
        }

        public void Update(List<T> entities)
        {
            dbSet.UpdateRange(entities);
        }

        public void BulkUpdate(List<T> entities)
        {
            _context.BulkUpdate(entities);
        }

        public Task BulkUpdateAsync(List<T> entities)
        {
            return _context.BulkUpdateAsync(entities);
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> objects = dbSet.Where(predicate).ToList();
            dbSet.RemoveRange(objects);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
