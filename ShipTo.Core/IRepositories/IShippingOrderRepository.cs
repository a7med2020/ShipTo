using ShipTo.Core.Entities;
using ShipTo.Core.IRepositories._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Core.IRepositories
{
    public interface IShippingOrderRepository : IBaseRepository<ShippingOrder>
    {
        List<ShippingOrder> Get();

        List<ShippingOrder> Get(string DeliveryStatusId, int? ShipperId, string ShippingOrderBulkName, string OrderNumber
          , int? CarrierId, DateTime? DeliveryDateFrom, DateTime? DeliveryDateTo);

        void Log(ShippingOrder shippingOrder);
    }
}
