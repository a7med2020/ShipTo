using Microsoft.AspNetCore.Mvc;
using ShipTo.Application.IServices;
using ShipTo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShipTo.Web.Controllers
{
    public class ShipperController : Controller
    {
        protected readonly IShipperService _shipperService;

        public ShipperController(IShipperService shipperService)
        {
            _shipperService = shipperService;
        }

        public ActionResult Index()
        {
            var shippers = _shipperService.Get();
            return View(shippers);
        }

        public IActionResult GetAll()
        {
            var shippers = _shipperService.Get();
            return View(shippers);
        }

        public IActionResult Add(Shipper shipper)
        {
            var result = _shipperService.Add(shipper);
            return View(result);
        }

        public IActionResult Update(Shipper shipper)
        {
            var result = _shipperService.Update(shipper);
            return View(result);
        }

        public IActionResult Delete(Shipper shipper)
        {
            var result = _shipperService.Delete(shipper);
            return View(result);
        }
    }
}
