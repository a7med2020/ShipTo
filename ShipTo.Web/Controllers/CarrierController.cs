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
    public class CarrierController : Controller
    {
        protected readonly ICarrierService _carrierService;

        public CarrierController(ICarrierService carrierService)
        {
            _carrierService = carrierService;
        }

        public ActionResult Index()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var carriers = _carrierService.Get();
            return View(carriers);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var carriers = _carrierService.Get();
            return Json(carriers);
        }

        public IActionResult GetById(int Id)
        {
            var carriers = _carrierService.Get(Id);
            return Json(carriers);
        }

        [HttpPost]
        public IActionResult AddUpdate(Carrier carrier)
        {
            if(carrier.ID == 0)
            {
                var result = _carrierService.Add(carrier);
                return Json(result);
            }
            else
            {
                var result = _carrierService.Update(carrier);
                return Json(result);
            }
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var result = _carrierService.Delete(Id);
            return Json(result);
        }
    }
}
