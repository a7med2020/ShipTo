﻿using ShipTo.Core;
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

namespace ShipTo.Infrastructure
{
    public class UnitOfWork :  IUnitOfWork
    {
        private readonly ShipToContext _context;

        public UnitOfWork(ShipToContext context)
        {
            _context = context;
            ShippingOrderRepository = new ShippingOrderRepository(_context);
            ShipperRepository = new BaseRepository<Shipper>(_context);
            DeliveryStatusRepository = new BaseRepository<DeliveryStatus>(_context);
            DeliveryStatusRepository = new BaseRepository<DeliveryStatus>(_context);
            CarrierRepository = new BaseRepository<Carrier>(_context);
            ShippingOrderColumnInfoRepository = new BaseRepository<ShippingOrderColumnInfo>(_context);
        }

        public IShippingOrderRepository ShippingOrderRepository { get; }

        public IBaseRepository<Shipper> ShipperRepository { get; }

        public IBaseRepository<DeliveryStatus> DeliveryStatusRepository { get; }

        public IBaseRepository<Carrier> CarrierRepository { get; }

        public IBaseRepository<ShippingOrderColumnInfo> ShippingOrderColumnInfoRepository { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
