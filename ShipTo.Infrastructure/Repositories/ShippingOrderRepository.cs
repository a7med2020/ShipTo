using Microsoft.EntityFrameworkCore;
using ShipTo.Core.Entities;
using ShipTo.Core.IRepositories;
using ShipTo.Infrastructure.Contexts;
using ShipTo.Infrastructure.Repositories._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.Entity;

namespace ShipTo.Infrastructure.Repositories
{
    public class ShippingOrderRepository : BaseRepository<ShippingOrder>, IShippingOrderRepository
    {
        private readonly ShipToContext _context;

        public ShippingOrderRepository(ShipToContext context) : base(context)
        {
            _context = context;
        }

        public void Log(ShippingOrder shippingOrder)
        {
            var ShippingOrderLog = new ShippingOrderLog()
            {
                ShippingOrderID = shippingOrder.ID,
                OrderNumber = shippingOrder.OrderNumber,
                BulkId = shippingOrder.BulkId,
                ShippingOrderBulkName = shippingOrder.ShippingOrderBulkName,
                OrderDate = shippingOrder.OrderDate,
                ClientName = shippingOrder.ClientName,
                ClientPhoneNumber = shippingOrder.ClientPhoneNumber,
                Direction = shippingOrder.Direction,
                Governorate = shippingOrder.Governorate,
                Address = shippingOrder.Address,
                ShipperId = shippingOrder.ShipperId,
                OrderDetails = shippingOrder.OrderDetails,
                OrderPiecesCount = shippingOrder.OrderPiecesCount,
                OrderTotalPrice = shippingOrder.OrderTotalPrice,
                DeliveryPrice = shippingOrder.DeliveryPrice,
                ShippingPrice = shippingOrder.ShippingPrice,
                OrderNetPrice = shippingOrder.OrderNetPrice,
                DeliveryStatusId = shippingOrder.DeliveryStatusId,
                DeliveryStatusReason = shippingOrder.DeliveryStatusReason,
                DeliveryDate = shippingOrder.DeliveryDate,
                CarrierId = shippingOrder.CarrierId,
                FileDataName = shippingOrder.FileDataName,
                CreatedBy = shippingOrder.CreatedBy,
                ModefiedBy = shippingOrder.ModefiedBy,
                CreatedDate = shippingOrder.CreatedDate,
                ModefiedDate = shippingOrder.ModefiedDate,
                IsDeleted = shippingOrder.IsDeleted,
                DeletedDate = shippingOrder.DeletedDate,
                DeletedBy = shippingOrder.DeletedBy,
                Notes = shippingOrder.Notes,
                 
            };
            _context.ShippingOrderLogs.Add(ShippingOrderLog);
        }

        public List<ShippingOrder> Get()
        {
            var a  = _context.ShippingOrders.Include(x => x.Shipper).Include(x=>x.DeliveryStatus).Include(x => x.Carrier).ToList();
            return _context.ShippingOrders.ToList();
        }

        public List<ShippingOrder> Get(string DeliveryStatusId, int ShipperId, string ShippingOrderBulkName, string OrderNumber
            , int CarrierId, DateTime? DeliveryDateFrom, DateTime? DeliveryDateTo)
        {
            var data = _context.ShippingOrders.Include(x => x.Shipper).Include(x => x.DeliveryStatus).Include(x => x.Carrier)
                .Where(x=> x.IsDeleted == false
                          && (x.DeliveryStatusId == DeliveryStatusId || DeliveryStatusId == "-1") 
                          && (x.ShipperId == ShipperId || ShipperId == -1)
                          && (ShippingOrderBulkName == null || x.ShippingOrderBulkName.Contains(ShippingOrderBulkName.Trim()))
                          && (OrderNumber == null  || x.OrderNumber == OrderNumber.Trim())
                          && (x.ShipperId == CarrierId || CarrierId == -1)
                          && (x.DeliveryDate >= DeliveryDateFrom && x.DeliveryDate <= DeliveryDateTo))
                .ToList();
            return data;
        }

    }
}
