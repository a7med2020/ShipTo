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
        List<ShippingOrder> Get();
        ShippingOrder Get(int Id);
        List<ShippingOrder> Get(string DeliveryStatusId, int? ShipperId, string ShippingOrderBulkName, string OrderNumber
          , int? CarrierId, DateTime? DeliveryDateFrom, DateTime? DeliveryDateTo);
        ReturnResultVM Add(ShippingOrder shippingOrder);
        ReturnResultVM Update(ShippingOrder shippingOrder);
        ReturnResultVM AddRange(List<ShippingOrder> shippingOrders);
        ReturnResultVM Delete(int Id);
        List<ShippingOrderLog> GetLog(int ShippingOrderId);

        ReturnResultVM UpdateCarrier(List<int> shippingOrderIds, int carrierId);
    }
}
