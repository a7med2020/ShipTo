using ShipTo.Core.Entities;
using ShipTo.Core.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Application.IServices
{
    public interface IShippingOrderService
    {
        ReturnResultVM AddRange(List<ShippingOrder> shippingOrders);
    }
}
