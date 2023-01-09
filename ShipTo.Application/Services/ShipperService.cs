using Microsoft.Data.SqlClient;
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
    public class ShipperService : IShipperService
    {
        protected readonly IUnitOfWork _unitOfWork;

        public ShipperService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Shipper> Get()
        {
            return _unitOfWork.ShipperRepository.GetAll(x => x.IsDeleted == false).ToList();
        }

        public Shipper Get(int Id)
        {
            return _unitOfWork.ShipperRepository.GetAll(x => x.IsDeleted == false && x.ID == Id).SingleOrDefault();
        }

        public ReturnResultVM Add(Shipper shipper)
        {
            try
            {
                if (_unitOfWork.ShipperRepository.Get(x => x.Name.Trim() == shipper.Name.Trim() && !x.IsDeleted ) == null )
                {
                    _unitOfWork.ShipperRepository.AddAsync(shipper);
                    _unitOfWork.Complete();
                    return new ReturnResultVM() { Status = ReturnResultStatusEnum.Success };
                }
                else
                {
                    return new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = "يوجد عنصر مسجل بنفس الاسم" };
                }
            }
            catch (Exception ex)
            {
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = "حدث خطأ" };
            }
        }

        public ReturnResultVM Update(Shipper shipper)
        {
            try
            {
                if (_unitOfWork.ShipperRepository.Get(x => x.Name.Trim() == shipper.Name.Trim() && x.ID != shipper.ID && !x.IsDeleted) == null)
                {
                    _unitOfWork.ShipperRepository.Update(shipper);
                    _unitOfWork.Complete();
                    return new ReturnResultVM() { Status = ReturnResultStatusEnum.Success };
                }
                else
                {
                    return new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = "يوجد عنصر مسجل بنفس الاسم" };
                }

            }
            catch (Exception ex)
            {
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = "حدث خطأ" };
            }
        }

        public ReturnResultVM Delete(int Id)
        {
            try
            {
                if (!_unitOfWork.ShippingOrderRepository.GetAll(x=>x.ShipperId == Id && !x.IsDeleted).Any())
                {
                    _unitOfWork.ShipperRepository.Delete(x => x.ID == Id);
                    _unitOfWork.Complete();
                    return new ReturnResultVM() { Status = ReturnResultStatusEnum.Success };
                }
                else
                {
                    return new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = "لا يمكن حذف شركة الشحن لأنه يوجد طلبات شحن مسجله عليها" };
                }
            }
              
            catch (Exception ex)
            {
                string Message = ex.Message;
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = "حدث خطأ" };
            }
        }
    }
}
