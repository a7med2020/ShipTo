using ShipTo.Core;
using ShipTo.Core.Entities;
using ShipTo.Core.Enums;
using ShipTo.Core.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Application.IServices
{
    public class ShippingOrderService : IShippingOrderService
    {
        protected readonly IUnitOfWork _unitOfWork;

        public ShippingOrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ReturnResultVM Add(ShippingOrder shippingOrder)
        {
            try
            {
                _unitOfWork.ShippingOrderRepository.BeginTransaction();
                _unitOfWork.ShippingOrderRepository.Add(shippingOrder);
                _unitOfWork.ShippingOrderRepository.Log(shippingOrder);
                _unitOfWork.Complete();
                _unitOfWork.ShippingOrderRepository.CommitTransaction();
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Success };
            }
            catch (Exception ex)
            {
                _unitOfWork.ShippingOrderRepository.RollbackTransaction();
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = "حدث خطأ" };
            }
        }

        public ReturnResultVM AddRange(List<ShippingOrder> shippingOrders)
        {
            try
            {
                _unitOfWork.ShippingOrderRepository.BeginTransaction();
                string BulkId = DateTime.Now.ToString("yyMMddHHmmss");
                foreach (var shippingOrder in shippingOrders)
                {
                    string lastOrderNumber = _unitOfWork.ShippingOrderRepository.GetAll().OrderBy(x => x.ID).Select(x=>x.OrderNumber).LastOrDefault();
                    Int64 orderNumber = string.IsNullOrEmpty(lastOrderNumber) ? 100000000001 : Convert.ToInt64(lastOrderNumber) + 1;
                    shippingOrder.OrderNumber = orderNumber.ToString();
                    shippingOrder.BulkId = BulkId;
                    _unitOfWork.ShippingOrderRepository.Add(shippingOrder);
                    _unitOfWork.Complete();
                    _unitOfWork.ShippingOrderRepository.Log(shippingOrder);
                }

                _unitOfWork.Complete();
                _unitOfWork.ShippingOrderRepository.CommitTransaction();
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Success };
            }
            catch (Exception ex)
            {
                _unitOfWork.ShippingOrderRepository.RollbackTransaction();
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = " حدث خطأ " + ":" + ex.Message };
            }
        }

        //public string GetShippingOrderNumber()
        //{

        //}
    }
}
