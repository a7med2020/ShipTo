using ShipTo.Core;
using ShipTo.Core.Entities;
using ShipTo.Core.Enums;
using ShipTo.Core.VMs;
using ShipTo.Infrastructure.UserResolverHandler;
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
        protected readonly IUserResolverHandler _userResolverHandler;

        public ShippingOrderService(IUnitOfWork unitOfWork, IUserResolverHandler userResolverHandler)
        {
            _unitOfWork = unitOfWork;
            _userResolverHandler = userResolverHandler;
        }
        public List<ShippingOrder> Get()
        {
            return _unitOfWork.ShippingOrderRepository.Get();
        }

        public ShippingOrder Get(int Id)
        {
            return _unitOfWork.ShippingOrderRepository.GetAll(x => x.IsDeleted == false && x.ID == Id).SingleOrDefault();
        }

        public List<ShippingOrder> Get(string DeliveryStatusId, int ShipperId, string ShippingOrderBulkName, string OrderNumber
          , int CarrierId, DateTime? DeliveryDateFrom, DateTime? DeliveryDateTo)
        {
            return _unitOfWork.ShippingOrderRepository.Get(DeliveryStatusId, ShipperId, ShippingOrderBulkName, OrderNumber
          , CarrierId, DeliveryDateFrom, DeliveryDateTo);
        }

        public ReturnResultVM AddNew(ShippingOrder shippingOrder)
        {
            string lastOrderNumber = _unitOfWork.ShippingOrderRepository.GetAll().OrderBy(x => x.ID).Select(x => x.OrderNumber).LastOrDefault();
            Int64 orderNumber = string.IsNullOrEmpty(lastOrderNumber) ? 100000000001 : Convert.ToInt64(lastOrderNumber) + 1;
            shippingOrder.OrderNumber = orderNumber.ToString();
            _unitOfWork.ShippingOrderRepository.Add(shippingOrder);
            _unitOfWork.Complete();
            _unitOfWork.ShippingOrderRepository.Log(shippingOrder);
            _unitOfWork.Complete();
            return new ReturnResultVM() { Status = ReturnResultStatusEnum.Success };
        }

        public ReturnResultVM Add(ShippingOrder shippingOrder)
        {
            try
            {
                shippingOrder.OrderDate = DateTime.Now;
                _unitOfWork.ShippingOrderRepository.BeginTransaction();
                //string BulkId = DateTime.Now.ToString("yyMMddHHmmss");
                //shippingOrder.BulkId = BulkId;
                AddNew(shippingOrder);
                _unitOfWork.Complete();
                _unitOfWork.ShippingOrderRepository.CommitTransaction();
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Success };
            }
            catch (Exception ex)
            {
                string Msg = ex.Message;
                _unitOfWork.ShippingOrderRepository.RollbackTransaction();
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = "حدث خطأ" };
            }
        }

        public ReturnResultVM Update(ShippingOrder shippingOrder)
        {
            try
            {
                _unitOfWork.ShippingOrderRepository.BeginTransaction();
                _unitOfWork.ShippingOrderRepository.Update(shippingOrder);
                _unitOfWork.Complete();
                _unitOfWork.ShippingOrderRepository.Log(shippingOrder);
                _unitOfWork.Complete();
                _unitOfWork.ShippingOrderRepository.CommitTransaction();
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Success };
            }
            catch (Exception ex)
            {
                string Msg = ex.Message;
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
                    shippingOrder.BulkId = BulkId;
                    AddNew(shippingOrder);
                }
                _unitOfWork.Complete();
                _unitOfWork.ShippingOrderRepository.CommitTransaction();
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Success };
            }
            catch (Exception ex)
            {
                string Msg = ex.Message;
                _unitOfWork.ShippingOrderRepository.RollbackTransaction();
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = " حدث خطأ " + ":" + ex.Message };
            }
        }

        public ReturnResultVM Delete(int Id)
        {
            try
            {
                _unitOfWork.ShippingOrderRepository.BeginTransaction();
                var ShippingOrder = _unitOfWork.ShippingOrderRepository.GetById(Id);
                _unitOfWork.ShipperRepository.Delete(x => x.ID == Id);
                ShippingOrder.IsDeleted = true;
                ShippingOrder.DeletedDate = DateTime.Now;
                ShippingOrder.DeletedBy = _userResolverHandler.GetUserId();
                _unitOfWork.ShippingOrderRepository.Log(ShippingOrder);
                _unitOfWork.Complete();
                _unitOfWork.ShippingOrderRepository.CommitTransaction();
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Success };
            }
            catch (Exception ex)
            {
                string Msg = ex.Message;
                _unitOfWork.ShippingOrderRepository.RollbackTransaction();
                string Message = ex.Message;
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = "حدث خطأ" };
            }
        }

        //public string GetShippingOrderNumber()
        //{

        //}
    }
}
