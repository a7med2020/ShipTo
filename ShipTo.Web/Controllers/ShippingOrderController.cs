﻿using Microsoft.AspNetCore.Mvc;
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
using Microsoft.AspNetCore.Hosting;
using AspNetCore.Reporting;

namespace ShipTo.Web.Controllers
{
    [Authorize]
    public class ShippingOrderController : Controller
    {
        protected readonly IShippingOrderService _shippingOrderService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ShippingOrderController(IShippingOrderService shippingOrderService, IWebHostEnvironment webHostEnvironment)
        {
            _shippingOrderService = shippingOrderService;
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public IActionResult Index()
        {
            return View(new List<ShippingOrder>());
        }

        [HttpGet]
        public IActionResult GetAll(string DeliveryStatusId,int? ShipperId, string ShippingOrderBulkName, string OrderNumber
            , int? CarrierId, DateTime? DeliveryDateFrom, DateTime? DeliveryDateTo)
        {
            var shippingOrders = _shippingOrderService.Get(DeliveryStatusId, ShipperId, ShippingOrderBulkName, OrderNumber, 
                CarrierId, DeliveryDateFrom, DeliveryDateTo);
            return Json(shippingOrders);
        }

        public IActionResult GetById(int Id)
        {
            var shippingOrder = _shippingOrderService.Get(Id);
            return Json(shippingOrder);
        }
        [HttpGet]
        public IActionResult Details(int Id)
        {
            var shippingOrder = _shippingOrderService.Get(Id);
            return View(shippingOrder);
        }

        public IActionResult GetLogById(int Id)
        {
            var shippingOrder = _shippingOrderService.GetLog(Id);
            return Json(shippingOrder);
        }

        [HttpPost]
        public IActionResult AddUpdate(ShippingOrder shippingOrder)
        {
            if (shippingOrder.ID == 0)
            {
                shippingOrder.OrderDate = DateTime.Now;
                var result = _shippingOrderService.Add(shippingOrder);
                return Json(result);
            }
            else
            {
                var result = _shippingOrderService.Update(shippingOrder);
                return Json(result);
            }
        }

        [HttpPost]
        public IActionResult ExtractToCarrier([FromBody] ExtractToCarrierVM extractToCarrierVM)
        {
            var result = _shippingOrderService.ExtractToCarrierAsExcelFile(extractToCarrierVM.ShippingOrderIds, extractToCarrierVM.Carrier_Id);
            return Json(result);
        }

        [HttpGet]
        public ActionResult ExtractToCarrierDownloadFile(string fileName)
        {
            try
            {
                if (fileName != null)
                {
                    var environmentRootPath = _webHostEnvironment.ContentRootPath;
                    var filePath = System.IO.Path.Combine(environmentRootPath, FolderPathEnum.ShippingOrderAddFromExcel, fileName);
                    byte[] fileByteArray = System.IO.File.ReadAllBytes(filePath);
                    return File(fileByteArray, "application/vnd.ms-excel", fileName);
                }
                else
                {
                    return Content("لم يتم إرسال اسم ملف");
                }
            }
            catch (Exception ex)
            {
                return Content("حدث خطأ غير معروف: " + ex.Message);
            }
        }

        #region AddFromExcel
        [HttpGet]
        public IActionResult AddFromExcel()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFromExcel(ShippingOrderAddFromExcelVM2 shippingOrderAddFromExcelVM)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                return Json(new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = "يوجد بيانات يجب إدخالها" });
            }
    
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
                        string dataValidationMessage = null;
                        string rowValidationMessage = null;

                        if (rowCount > 1)
                        {
                            for (var row = 2; row <= rowCount; row++)
                            {
                                rowValidationMessage = null;
                                try
                                {
                                    ShippingOrder shippingOrder = new ShippingOrder();
                                    shippingOrder.ShippingOrderBulkName = shippingOrderAddFromExcelVM.ShippingOrderBulkName;
                                    shippingOrder.ShipperId = shippingOrderAddFromExcelVM.ShipperId;
                                    shippingOrder.DeliveryStatusId = DeliveryStatusEnum.UnderDelivery;//shippingOrderAddFromExcelVM.DeliveryStatusId;
                                    shippingOrder.DeliveryDate = shippingOrderAddFromExcelVM.DeliveryDate;//shippingOrderAddFromExcelVM.DeliveryStatusId;
                                    //shippingOrder.CarrierId = shippingOrderAddFromExcelVM.CarrierId;
                                    shippingOrder.OrderDate = DateTime.Now;
                                    shippingOrder.FileDataName = excelFile.FileName;
                                    
                                    if (Convert.ToInt32(shippingOrderAddFromExcelVM.ClientNameColName) > 0)
                                    {
                                        shippingOrder.ClientName = Convert.ToString(worksheet.Cells[row, Convert.ToInt32(shippingOrderAddFromExcelVM.ClientNameColName)].Value);
                                        shippingOrder.ClientName = string.IsNullOrEmpty(shippingOrder.ClientName) ? null : shippingOrder.ClientName.Trim();
                                        if (shippingOrder.ClientName == null)
                                            rowValidationMessage += "يجب إدخال اسم العميل" + "<br/>";
                                    }
                                    if (Convert.ToInt32(shippingOrderAddFromExcelVM.ClientPhoneNumberColName) > 0)
                                    {
                                        shippingOrder.ClientPhoneNumber = Convert.ToString(worksheet.Cells[row, Convert.ToInt32(shippingOrderAddFromExcelVM.ClientPhoneNumberColName)].Value);
                                        shippingOrder.ClientPhoneNumber = string.IsNullOrEmpty(shippingOrder.ClientPhoneNumber) ? null : shippingOrder.ClientPhoneNumber.Trim();
                                        if (shippingOrder.ClientPhoneNumber == null)
                                            rowValidationMessage += "يجب إدخال هاتف العميل" + "<br/>";
                                    }
                                    if (Convert.ToInt32(shippingOrderAddFromExcelVM.DirectionColName) > 0) 
                                    { 
                                        shippingOrder.Direction = Convert.ToString(worksheet.Cells[row, Convert.ToInt32(shippingOrderAddFromExcelVM.DirectionColName)].Value);
                                        shippingOrder.Direction = string.IsNullOrEmpty(shippingOrder.Direction) ? null : shippingOrder.Direction.Trim();
                                    }
                                    if (Convert.ToInt32(shippingOrderAddFromExcelVM.GovernorateColName) > 0)
                                    {
                                        shippingOrder.Governorate = Convert.ToString(worksheet.Cells[row, Convert.ToInt32(shippingOrderAddFromExcelVM.GovernorateColName)].Value);
                                        shippingOrder.Governorate = string.IsNullOrEmpty(shippingOrder.Governorate) ? null : shippingOrder.Governorate.Trim();
                                    }
                                    if (Convert.ToInt32(shippingOrderAddFromExcelVM.AddressColName) > 0)
                                    {
                                        shippingOrder.Address = Convert.ToString(worksheet.Cells[row, Convert.ToInt32(shippingOrderAddFromExcelVM.AddressColName)].Value);
                                        shippingOrder.Address = string.IsNullOrEmpty(shippingOrder.Address) ? null : shippingOrder.Address.Trim();
                                        if (shippingOrder.Address == null)
                                            rowValidationMessage += "يجب إدخال العنوان" + "<br/>";
                                    }
                                    if (Convert.ToInt32(shippingOrderAddFromExcelVM.OrderDetailsColName) > 0)
                                    {
                                        shippingOrder.OrderDetails = Convert.ToString(worksheet.Cells[row, Convert.ToInt32(shippingOrderAddFromExcelVM.OrderDetailsColName)].Value);
                                        shippingOrder.OrderDetails = string.IsNullOrEmpty(shippingOrder.OrderDetails) ? null : shippingOrder.OrderDetails.Trim();
                                        if (shippingOrder.OrderDetails == null)
                                            rowValidationMessage += "يجب إدخال تفاصيل الطلب" + "<br/>";
                                    }
                                    if (Convert.ToInt32(shippingOrderAddFromExcelVM.OrderPiecesCountColName) > 0)
                                    {
                                        shippingOrder.OrderPiecesCount = Convert.ToInt32(worksheet.Cells[row, Convert.ToInt32(shippingOrderAddFromExcelVM.OrderPiecesCountColName)].Value);
                                        shippingOrder.OrderPiecesCount = string.IsNullOrEmpty(Convert.ToString(shippingOrder.OrderPiecesCount)) ? null : (int?)shippingOrder.OrderPiecesCount;
                                    }
                                    if (Convert.ToInt32(shippingOrderAddFromExcelVM.OrderTotalPriceColName) > 0)
                                    {
                                        decimal _orderTotalPrice = 0;
                                        var isDecimal = decimal.TryParse(Convert.ToString(worksheet.Cells[row, Convert.ToInt32(shippingOrderAddFromExcelVM.OrderTotalPriceColName)].Value),out _orderTotalPrice);
                                        shippingOrder.OrderTotalPrice = _orderTotalPrice;
                                        if (isDecimal == false || _orderTotalPrice == 0)
                                            rowValidationMessage += "سعر الطلب يجب أن يكون رقم أكبر من صفر" + "<br/>";
                                    }
                                    if (Convert.ToInt32(shippingOrderAddFromExcelVM.ShippingPriceColName) > 0)
                                    {
                                        decimal _shippingPrice = 0;
                                        var isDecimal = decimal.TryParse(Convert.ToString(worksheet.Cells[row, Convert.ToInt32(shippingOrderAddFromExcelVM.ShippingPriceColName)].Value),out _shippingPrice);
                                        shippingOrder.ShippingPrice = _shippingPrice;
                                        if (isDecimal == false || _shippingPrice == 0)
                                            rowValidationMessage += "سعر الشحن يجب أن يكون رقم أكبر من صفر" + "<br/>";
                                    }
                                    if (Convert.ToInt32(shippingOrderAddFromExcelVM.OrderNetPriceColName) > 0)
                                    {
                                        decimal _orderNetPrice = 0;
                                        var isDecimal = decimal.TryParse(Convert.ToString(worksheet.Cells[row, Convert.ToInt32(shippingOrderAddFromExcelVM.OrderNetPriceColName)].Value),out _orderNetPrice);
                                        shippingOrder.OrderNetPrice = _orderNetPrice;
                                        if (isDecimal == false || _orderNetPrice == 0)
                                            rowValidationMessage += "سعر المنتج يجب أن يكون رقم أكبر من صفر" + "<br/>";
                                    }
                                    if (Convert.ToInt32(shippingOrderAddFromExcelVM.NotesColName) > 0)
                                    {
                                        shippingOrder.Notes = Convert.ToString(worksheet.Cells[row, Convert.ToInt32(shippingOrderAddFromExcelVM.NotesColName)].Value);
                                        shippingOrder.Notes = string.IsNullOrEmpty(shippingOrder.Notes) ? null : shippingOrder.Notes.Trim();
                                    }
                                    shippingOrders.Add(shippingOrder);
                                    if (!string.IsNullOrEmpty(rowValidationMessage))
                                        dataValidationMessage += "صف رقم: " + row + "<br/>" + rowValidationMessage + "<br/>";
                                }
                                catch (Exception ex)
                                {
                                    return Json(new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = "يوجد خطأ في بيانات الملف" });
                                }
                            }
                            // Validation level 1 : When read the data forn the file into ShippingOrder Object 
                            if (!string.IsNullOrEmpty(dataValidationMessage))
                                return Json(new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = dataValidationMessage });
                            // Validation level 2 : Validation Context to insure that data is corrected
                            //string validationMessage = _shippingOrderService.VidateExcelData(shippingOrders);
                            //if(!string.IsNullOrEmpty(validationMessage))
                            //    return Json(new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = validationMessage });
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
        #endregion

        FileContentResult CreateInvoice(List<int> shippingOrderIds)
        {
            string mimType = "";
            var reportPath = _webHostEnvironment.WebRootPath + "\\Reports\\dc_ShippingOrderInvoice.rdlc";
            LocalReport localReport = new LocalReport(reportPath);
            var shippingOrders = _shippingOrderService.GetForInvoice(shippingOrderIds);
            localReport.AddDataSource("DS_ShippingOrderInvoice", shippingOrders);
            var result = localReport.Execute(RenderType.Pdf, 1, null, mimType);

            string fileName = "Invoice";
            if (shippingOrders.Count == 1)
              fileName =  "فاتورة طلب رقم" + shippingOrders.FirstOrDefault().OrderNumber + ".pdf";
            else if (shippingOrders.Count > 1)
              fileName = $"فاتورة مجمعة.pdf";

            // Response...
            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = fileName,
                Inline = true  // false = prompt the user for downloading;  true = browser to try to show the file inline
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            Response.Headers.Add("X-Content-Type-Options", "nosniff");

            //return File(System.IO.File.ReadAllBytes(file), "application/pdf");

             return File(result.MainStream, "application/pdf");
        }

         
        [HttpGet]
        public IActionResult Print(string Ids)
        {
            
            List<int> _Ids = Ids?.Split(',')?.Select(Int32.Parse)?.ToList();
            return CreateInvoice(_Ids);
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var result = _shippingOrderService.Delete(Id);
            return Json(result);
        }
    }

    public class ShippingOrderAddFromExcelVM2 : ShippingOrderAddFromExcelVM
    {
        public IFormFile ShippingExcelFile { get; set; }

    }
}
