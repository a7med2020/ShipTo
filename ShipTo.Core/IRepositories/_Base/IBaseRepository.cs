using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Core.IRepositories._Base
{
    public interface IBaseRepository<T> where T : class
    {
        //////////////////////////////////////////////// Read Funections /////////////////////////////////////////////////////////////////
        // Get an entity by int id
        T GetById(object id, Expression<Func<T, object>> includeExpression = null);
        Task<T> GetByIdAsync(object id, Expression<Func<T, object>> includeExpression = null);

        T Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        T AsNoTracking(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<T> AsNoTrackingAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        IQueryable<T> AsNoTrackingMany(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<IQueryable<T>> AsNoTrackingManyAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        T GetSingle(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include);
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include);
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null);
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
           string orderBy = null, string orderDirection = "asc", int? skip = null, int? take = null);

        Task<IQueryable<T>> GetAllAsync();
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<IQueryable<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include);
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include);
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null);
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            string orderBy = null, string orderDirection = "asc", int? skip = null, int? take = null);

        int Count();
        int Count(Expression<Func<T, bool>> predicate);

        /////////////////////////////////////////////// Write functions //////////////////////////////////////////////////////////


        void Add(T entity);
        void Add(List<T> entities);

        void AddAsync(T entity);
        Task AddAsync(List<T> entities);

        void BulkInsert(List<T> entities);
        Task BulkInsertAsync(List<T> entities);

        T Update(T entity);
        void Update(List<T> entities);

        void BulkUpdate(List<T> entities);
        Task BulkUpdateAsync(List<T> entities);

        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);



        //Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        //Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        //Task<int> CompleteAsync(CancellationToken cancellationToken = default);

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
         
    }
}
