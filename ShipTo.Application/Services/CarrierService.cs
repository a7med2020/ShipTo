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
    public class CarrierService : ICarrierService
    {
        protected readonly IUnitOfWork _unitOfWork;

        public CarrierService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Carrier> Get()
        {
            return _unitOfWork.CarrierRepository.GetAll(x => x.IsDeleted == false).ToList();
        }

        public Carrier Get(int Id)
        {
            return _unitOfWork.CarrierRepository.GetAll(x => x.IsDeleted == false && x.ID == Id).SingleOrDefault();
        }

        public ReturnResultVM Add(Carrier carrier)
        {
            try
            {
                if (_unitOfWork.CarrierRepository.Get(x => x.Name.Trim() == carrier.Name.Trim() && !x.IsDeleted) == null)
                {
                    _unitOfWork.CarrierRepository.AddAsync(carrier);
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

        public ReturnResultVM Update(Carrier carrier)
        {
            try
            {
                if (_unitOfWork.CarrierRepository.Get(x => x.Name.Trim() == carrier.Name.Trim() && x.ID != carrier.ID && !x.IsDeleted) == null)
                {
                    _unitOfWork.CarrierRepository.Update(carrier);
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
                if (!_unitOfWork.ShippingOrderRepository.GetAll(x => x.CarrierId == Id && !x.IsDeleted).Any())
                {
                    _unitOfWork.CarrierRepository.Delete(x => x.ID == Id);
                    _unitOfWork.Complete();
                    return new ReturnResultVM() { Status = ReturnResultStatusEnum.Success };
                }
                else
                {
                    return new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = "لا يمكن حذف المندوب لأنه يوجد طلبات شحن مسجله عليه" };
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
