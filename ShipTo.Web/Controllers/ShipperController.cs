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
    public class ShipperController : Controller
    {
        protected readonly IShipperService _shipperService;

        public ShipperController(IShipperService shipperService)
        {
            _shipperService = shipperService;
        }

        public ActionResult Index()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var shippers = _shipperService.Get();
            return View(shippers);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var shippers = _shipperService.Get();
            return Json(shippers);
        }

        public IActionResult GetById(int Id)
        {
            var shippers = _shipperService.Get(Id);
            return Json(shippers);
        }

        [HttpPost]
        public IActionResult AddUpdate(Shipper shipper)
        {
            if(shipper.ID == 0)
            {
                var result = _shipperService.Add(shipper);
                return Json(result);
            }
            else
            {
                var result = _shipperService.Update(shipper);
                return Json(result);
            }
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var result = _shipperService.Delete(Id);
            return Json(result);
        }
    }
}
