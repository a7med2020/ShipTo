using ShipTo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipTo.Core.Entities;
using ShipTo.Application.IServices.ItemServices;
using ShipTo.Core;

namespace ShipTo.Application.Services.ItemServices
{
    public class ItemService : IItemService
    {
        protected readonly IUnitOfWork _unitOfWork;

        public ItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public async Task<List<Item>> GetAll(){
        //    return await _unitOfWork.ItemRepository.GetAllAsync();
        //}
    }
}
