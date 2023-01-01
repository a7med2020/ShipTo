using Microsoft.AspNetCore.Mvc;
using ShipTo.Application.IServices;
using ShipTo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using OfficeOpenXml;
using ShipTo.Core.VMs;
using ShipTo.Core.Enums;
using ShipTo.Application.Utilities;

namespace ShipTo.Web.Controllers
{
    [Authorize]
    public class ShippingOrderController : Controller
    {
        protected readonly IShippingOrderService _shippingOrderService;

        public ShippingOrderController(IShippingOrderService shippingOrderService)
        {
            _shippingOrderService = shippingOrderService;
        }

        public IActionResult Index()
        {
            //var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var shippingOrders = _shippingOrderService.Get();
            return View();
        }
        [HttpGet]
        public IActionResult AddFromExcel()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFromExcel(ShippingOrderAddFromExcelVM2 shippingOrderAddFromExcelVM)
        {
            IFormFile excelFile = shippingOrderAddFromExcelVM.ShippingExcelFile;

            if (excelFile?.Length > 0
                 && (Path.GetExtension(excelFile.FileName) == ".xlsx"
                 || Path.GetExtension(excelFile.FileName) == ".xlx"))
            {
                var stream = excelFile.OpenReadStream();
                List<ShippingOrder> shippingOrders = new List<ShippingOrder>();
                try
                {
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets.First();
                        var rowCount = worksheet.Dimension?.Rows;
                        if (rowCount > 1)
                        {
                            for (var row = 2; row <= rowCount; row++)
                            {
                                try
                                {
                                    ShippingOrder shippingOrder = new ShippingOrder();
                                    shippingOrder.ShippingOrderBulkName = shippingOrderAddFromExcelVM.ShippingOrderBulkName;
                                    shippingOrder.ShipperId = shippingOrderAddFromExcelVM.ShipperId;
                                    shippingOrder.DeliveryStatusId = DeliveryStatusEnum.UnderDelivery;//shippingOrderAddFromExcelVM.DeliveryStatusId;
                                    //shippingOrder.CarrierId = shippingOrderAddFromExcelVM.CarrierId;
                                    shippingOrder.OrderDate = DateTime.Now;
                                    shippingOrder.Notes = shippingOrderAddFromExcelVM.Notes;
                                    if (Convert.ToInt32(shippingOrderAddFromExcelVM.ClientNameColName) > 0)
                                        shippingOrder.ClientName = Convert.ToString(worksheet.Cells[row, Convert.ToInt32(shippingOrderAddFromExcelVM.ClientNameColName)].Value);
                                    if (Convert.ToInt32(shippingOrderAddFromExcelVM.ClientPhoneNumberColName) > 0)
                                        shippingOrder.ClientPhoneNumber =  Convert.ToString(worksheet.Cells[row, Convert.ToInt32(shippingOrderAddFromExcelVM.ClientPhoneNumberColName)].Value);
                                    if (Convert.ToInt32(shippingOrderAddFromExcelVM.DirectionColName) > 0)
                                        shippingOrder.Direction = Convert.ToString(worksheet.Cells[row, Convert.ToInt32(shippingOrderAddFromExcelVM.DirectionColName)].Value);
                                    if (Convert.ToInt32(shippingOrderAddFromExcelVM.GovernorateColName) > 0)
                                        shippingOrder.Governorate = Convert.ToString(worksheet.Cells[row, Convert.ToInt32(shippingOrderAddFromExcelVM.GovernorateColName)].Value);
                                    if (Convert.ToInt32(shippingOrderAddFromExcelVM.AddressColName) > 0)
                                        shippingOrder.Address = Convert.ToString(worksheet.Cells[row, Convert.ToInt32(shippingOrderAddFromExcelVM.AddressColName)].Value);
                                    if (Convert.ToInt32(shippingOrderAddFromExcelVM.OrderDetailsColName) > 0)
                                        shippingOrder.OrderDetails = Convert.ToString(worksheet.Cells[row, Convert.ToInt32(shippingOrderAddFromExcelVM.OrderDetailsColName)].Value);
                                    if (Convert.ToInt32(shippingOrderAddFromExcelVM.OrderPiecesCountColName) > 0)
                                        shippingOrder.OrderPiecesCount = Convert.ToInt32(worksheet.Cells[row, Convert.ToInt32(shippingOrderAddFromExcelVM.OrderPiecesCountColName)].Value);
                                    if (Convert.ToInt32(shippingOrderAddFromExcelVM.OrderTotalPriceColName) > 0)
                                        shippingOrder.OrderTotalPrice = decimal.Parse(Convert.ToString(worksheet.Cells[row, Convert.ToInt32(shippingOrderAddFromExcelVM.OrderTotalPriceColName)].Value));
                                    if (Convert.ToInt32(shippingOrderAddFromExcelVM.ShippingPriceColName) > 0)
                                        shippingOrder.ShippingPrice = decimal.Parse(Convert.ToString(worksheet.Cells[row, Convert.ToInt32(shippingOrderAddFromExcelVM.ShippingPriceColName)].Value));
                                    if (Convert.ToInt32(shippingOrderAddFromExcelVM.OrderNetPriceColName) > 0)
                                        shippingOrder.OrderNetPrice = decimal.Parse(Convert.ToString(worksheet.Cells[row, Convert.ToInt32(shippingOrderAddFromExcelVM.OrderNetPriceColName)].Value));

                                    shippingOrders.Add(shippingOrder);
                                }

                                catch (Exception ex)
                                {
                                    return Json(new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = "يوجد خطأ في بيانات الملف" });
                                }
                            }
                        }
                        else
                        {
                            return Json(new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = "ملف الإكسل فارغ!!" });
                        }

                    }
                    var addResult = _shippingOrderService.AddRange(shippingOrders);
                    if(addResult.Status == ReturnResultStatusEnum.Success)
                    return Json(new ReturnResultVM() { Status = ReturnResultStatusEnum.Success, ErrorMessage = null, DataObj = null });
                    else
                        return Json(addResult);
                }
                catch (Exception e)
                {
                    return Json(new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = "حدث خطأ اثناء معاجلة الملف: " + e.Message });
                }
            }
            else
            {
                return  Json(new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = "يجب اختيار ملف إكسل!" });
            }
             
        }

        [HttpPost]
        public IActionResult GetExcelFileColumns(IFormFile shippingExcelFile)
        {
            if (shippingExcelFile?.Length > 0
                  && (Path.GetExtension(shippingExcelFile.FileName) == ".xlsx"
                  || Path.GetExtension(shippingExcelFile.FileName) == ".xlx"))
            {
                var stream = shippingExcelFile.OpenReadStream();
                List<ReturnResultVM> memberRecurringPaymentAmountVMs = new List<ReturnResultVM>();
                try
                {
                    Dictionary<string, string> columnNames = new Dictionary<string, string>();
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets.First();
                        var rowCount = worksheet.Dimension?.Rows;
                        if (rowCount > 1)
                        {
                            columnNames = GetHeaderColumns(worksheet);
                        }
                        else
                        {
                            return Json(new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = "ملف الإكسل فارغ!!" });
                        }

                    }

                    return Json(new ReturnResultVM() { Status = ReturnResultStatusEnum.Success, ErrorMessage = null, DataObj = columnNames });
                }
                catch (Exception e)
                {
                    return Json(new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = "حدث خطأ اثناء معاجلة الملف: " + e.Message });
                }
            }
            else
            {
                return Json(new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = "يجب اختيار ملف إكسل!" });
            }
        }

        public static Dictionary<string, string> GetHeaderColumns(ExcelWorksheet sheet)
        {
            int index = 1;
            Dictionary<string, string> columns = new Dictionary<string, string>();
            foreach (var firstRowCell in sheet.Cells[sheet.Dimension.Start.Row, sheet.Dimension.Start.Column, 1, sheet.Dimension.End.Column])
            {
                columns.Add(index.ToString(), firstRowCell.Text);
                index++;
            }

            return columns;
        }


    }

    public class ShippingOrderAddFromExcelVM2 : ShippingOrderAddFromExcelVM
    {
        public IFormFile ShippingExcelFile { get; set; }

    }
}
