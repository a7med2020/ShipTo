using ShipTo.Core;
using ShipTo.Core.Entities;
using ShipTo.Core.IRepositories;
using ShipTo.Core.IRepositories._Base;
using ShipTo.Infrastructure.Contexts;
using ShipTo.Infrastructure.Repositories;
using ShipTo.Infrastructure.Repositories._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using ShipTo.Infrastructure.UserResolverHandler;
using ShipTo.Infrastructure.Extentions;

namespace ShipTo.Infrastructure
{
    public class UnitOfWork :  IUnitOfWork
    {
        private readonly ShipToContext _context;
        private readonly IUserResolverHandler _userResolverHandler;
        public UnitOfWork(ShipToContext context)
        {
          
        }

        public UnitOfWork(ShipToContext context, IUserResolverHandler userResolverHandler)  
        {
            _context = context;
            ShippingOrderRepository = new ShippingOrderRepository(_context);
            ShipperRepository = new BaseRepository<Shipper>(_context);
            DeliveryStatusRepository = new BaseRepository<DeliveryStatus>(_context);
            DeliveryStatusRepository = new BaseRepository<DeliveryStatus>(_context);
            CarrierRepository = new BaseRepository<Carrier>(_context);
            ShippingOrderColumnInfoRepository = new BaseRepository<ShippingOrderColumnInfo>(_context);
            ShippingOrderLogRepository = new BaseRepository<ShippingOrderLog>(_context);
            _userResolverHandler = userResolverHandler;
        }

        public IShippingOrderRepository ShippingOrderRepository { get; }

        public IBaseRepository<Shipper> ShipperRepository { get; }

        public IBaseRepository<DeliveryStatus> DeliveryStatusRepository { get; }

        public IBaseRepository<Carrier> CarrierRepository { get; }

        public IBaseRepository<ShippingOrderColumnInfo> ShippingOrderColumnInfoRepository { get; }

        public IBaseRepository<ShippingOrderLog> ShippingOrderLogRepository { get; }

        public int Complete()
        {
            // var entities = (from entry in ChangeTracker.Entries()
            //                where entry.State == EntityState.Modified || entry.State == EntityState.Added
            //                select entry.Entity);

            //var validationResults = new List<ValidationResult>();
            //foreach (var entity in entities)
            //{
            //    if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults))
            //    {
                     
            //    }
            //}
            _context.ChangeTracker.ApplyAuditInformation(_userResolverHandler);
            return _context.SaveChanges();
        }
      
        public Task<int> CompleteAsync()
        {
            _context.ChangeTracker.ApplyAuditInformation(_userResolverHandler);
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
