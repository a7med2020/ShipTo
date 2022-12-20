using ShipTo.Core.Entities;
using ShipTo.Core.IRepositories;
using ShipTo.Core.IRepositories._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IShippingOrderRepository ShippingOrderRepository { get; }
        IBaseRepository<Shipper> ShipperRepository { get; }
        IBaseRepository<DeliveryStatus> DeliveryStatusRepository { get; }
        IBaseRepository<Carrier> CarrierRepository { get; }
        IBaseRepository<ShippingOrderColumnInfo> ShippingOrderColumnInfoRepository { get; }
        int Complete();
    }
}
