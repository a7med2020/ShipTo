using Microsoft.EntityFrameworkCore;
using ShipTo.Core.IRepositories._Base;
using ShipTo.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Infrastructure.Repositories._Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ShipToContext _ShipToContext;

        public BaseRepository(ShipToContext employeeContext)
        {
            _ShipToContext = employeeContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _ShipToContext.Set<T>().AddAsync(entity);
            await _ShipToContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _ShipToContext.Set<T>().Remove(entity);
            await _ShipToContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _ShipToContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _ShipToContext.Set<T>().FindAsync(id);
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
