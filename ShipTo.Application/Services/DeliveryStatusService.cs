using ShipTo.Application.IServices;
using ShipTo.Core;
using ShipTo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Application.Services
{
    public class DeliveryStatusService : IDeliveryStatusService
    {
        protected readonly IUnitOfWork _unitOfWork;

        public DeliveryStatusService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<DeliveryStatus> Get()
        {
            return _unitOfWork.DeliveryStatusRepository.GetAll().ToList();
        }

    }
}
