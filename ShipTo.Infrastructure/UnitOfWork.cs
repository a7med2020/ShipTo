using ShipTo.Core;
using ShipTo.Core.Entities;
using ShipTo.Core.IRepositories._Base;
using ShipTo.Infrastructure.Contexts;
using ShipTo.Infrastructure.Repositories._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShipToContext _ShipToContext;

        public UnitOfWork(ShipToContext context)
        {
            _ShipToContext = context;
            //ItemRepository = new BaseRepository<Item>(_ShipToContext);
        }

        //public IBaseRepository<Item> ItemRepository { get; }

        public int Complete()
        {
            return _ShipToContext.SaveChanges();
        }

        public void Dispose()
        {
            _ShipToContext.Dispose();
        }
    }
}
