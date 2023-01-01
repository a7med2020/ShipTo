using Microsoft.AspNetCore.Mvc;
using ShipTo.Application.IServices;
using ShipTo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ShipTo.Web.Controllers
{
    [Authorize]
    public class DeliveryStatusController : Controller
    {
        protected readonly IDeliveryStatusService _deliveryStatusService;

        public DeliveryStatusController(IDeliveryStatusService deliveryStatusService)
        {
            _deliveryStatusService = deliveryStatusService;
        }
 
        [HttpGet]
        public IActionResult GetAll()
        {
            var deliveryStatuss = _deliveryStatusService.Get();
            return Json(deliveryStatuss);
        }

        //public IActionResult GetById(int Id)
        //{
        //    var deliveryStatuss = _deliveryStatusService.Get(Id);
        //    return Json(deliveryStatuss);
        //}

      
    }
}
