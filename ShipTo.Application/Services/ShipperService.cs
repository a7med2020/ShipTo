using ShipTo.Core;
using ShipTo.Core.Entities;
using ShipTo.Core.Enums;
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

        public string Add(Shipper shipper)
        {
            try
            {

                _unitOfWork.ShipperRepository.AddAsync(shipper);

                return ReturnResultStatusEnum.Success;
            }
            catch (Exception ex)
            {
                string Message = ex.Message;
                return ReturnResultStatusEnum.Failure;
            }
        }

        public string Update(Shipper shipper)
        {
            try
            {
                _unitOfWork.ShipperRepository.Update(shipper);

                return ReturnResultStatusEnum.Success;
            }
            catch (Exception ex)
            {
                string Message = ex.Message;
                return ReturnResultStatusEnum.Failure;
            }
        }

        public string Delete(Shipper shipper)
        {
            try
            {
                _unitOfWork.ShipperRepository.Delete(shipper);

                return ReturnResultStatusEnum.Success;
            }
            catch (Exception ex)
            {
                string Message = ex.Message;
                return ReturnResultStatusEnum.Failure;
            }
        }
    }
}
