using Microsoft.Extensions.Configuration;
using ShipTo.Application.IServices;
using ShipTo.Core;
using ShipTo.Core.Entities;
using ShipTo.Core.Enums;
using ShipTo.Core.VMs;
using ShipTo.Infrastructure.UserResolverHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Application.Services
{
    public class ShippingOrderService : IShippingOrderService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IFileManagementService _fileManagementService;
        protected readonly IUserResolverHandler _userResolverHandler;
        private readonly IConfiguration _configuration;
        public ShippingOrderService(IUnitOfWork unitOfWork, IUserResolverHandler userResolverHandler, IFileManagementService fileManagementService, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _fileManagementService = fileManagementService;
            _userResolverHandler = userResolverHandler;
            _configuration = configuration;
        }
        public List<ShippingOrder> Get()
        {
            return _unitOfWork.ShippingOrderRepository.Get();
        }

        public ShippingOrder Get(int Id)
        {
            return _unitOfWork.ShippingOrderRepository.GetAll(x => x.IsDeleted == false && x.ID == Id).SingleOrDefault();
        }

        public List<ShippingOrder> Get(string DeliveryStatusId, int? ShipperId, string ShippingOrderBulkName, string OrderNumber
          , int? CarrierId, DateTime? DeliveryDateFrom, DateTime? DeliveryDateTo)
        {
            return _unitOfWork.ShippingOrderRepository.Get(DeliveryStatusId, ShipperId, ShippingOrderBulkName, OrderNumber
          , CarrierId, DeliveryDateFrom, DeliveryDateTo);
        }

        public ReturnResultVM AddNew(ShippingOrder shippingOrder)
        {
            string lastOrderNumber = _unitOfWork.ShippingOrderRepository.GetAll().OrderBy(x => x.ID).Select(x => x.OrderNumber).LastOrDefault();
            Int64 orderNumber = string.IsNullOrEmpty(lastOrderNumber) ? 100000000001 : Convert.ToInt64(lastOrderNumber) + 1;
            shippingOrder.OrderNumber = orderNumber.ToString();
            _unitOfWork.ShippingOrderRepository.Add(shippingOrder);
            _unitOfWork.Complete();
            _unitOfWork.ShippingOrderRepository.Log(shippingOrder);
            _unitOfWork.Complete();
            return new ReturnResultVM() { Status = ReturnResultStatusEnum.Success };
        }

        public ReturnResultVM Add(ShippingOrder shippingOrder)
        {
            try
            {
                _unitOfWork.ShippingOrderRepository.BeginTransaction();
                //string BulkId = DateTime.Now.ToString("yyMMddHHmmss");
                //shippingOrder.BulkId = BulkId;
                AddNew(shippingOrder);
                _unitOfWork.Complete();
                _unitOfWork.ShippingOrderRepository.CommitTransaction();
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Success };
            }
            catch (Exception ex)
            {
                string Msg = ex.Message;
                _unitOfWork.ShippingOrderRepository.RollbackTransaction();
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = "حدث خطأ" };
            }
        }

        public ReturnResultVM Update(ShippingOrder shippingOrder)
        {
            try
            {
                _unitOfWork.ShippingOrderRepository.BeginTransaction();
                _unitOfWork.ShippingOrderRepository.Update(shippingOrder);
                _unitOfWork.Complete();
                _unitOfWork.ShippingOrderRepository.Log(shippingOrder);
                _unitOfWork.Complete();
                _unitOfWork.ShippingOrderRepository.CommitTransaction();
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Success };
            }
            catch (Exception ex)
            {
                string Msg = ex.Message;
                _unitOfWork.ShippingOrderRepository.RollbackTransaction();
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = "حدث خطأ" };
            }
        }


        public ReturnResultVM AddRange(List<ShippingOrder> shippingOrders)
        {
            try
            {
                _unitOfWork.ShippingOrderRepository.BeginTransaction();
                string BulkId = DateTime.Now.ToString("yyMMddHHmmss");
                foreach (var shippingOrder in shippingOrders)
                {
                    shippingOrder.BulkId = BulkId;
                    AddNew(shippingOrder);
                }
                _unitOfWork.Complete();
                _unitOfWork.ShippingOrderRepository.CommitTransaction();
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Success };
            }
            catch (Exception ex)
            {
                string Msg = ex.Message;
                _unitOfWork.ShippingOrderRepository.RollbackTransaction();
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = " حدث خطأ " + ":" + ex.Message };
            }
        }

        public ReturnResultVM Delete(int Id)
        {
            try
            {
                _unitOfWork.ShippingOrderRepository.BeginTransaction();
                var ShippingOrder = _unitOfWork.ShippingOrderRepository.GetById(Id);
                _unitOfWork.ShipperRepository.Delete(x => x.ID == Id);
                ShippingOrder.IsDeleted = true;
                ShippingOrder.DeletedDate = DateTime.Now;
                ShippingOrder.DeletedBy = _userResolverHandler.GetUserId();
                _unitOfWork.ShippingOrderRepository.Log(ShippingOrder);
                _unitOfWork.Complete();
                _unitOfWork.ShippingOrderRepository.CommitTransaction();
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Success };
            }
            catch (Exception ex)
            {
                string Msg = ex.Message;
                _unitOfWork.ShippingOrderRepository.RollbackTransaction();
                string Message = ex.Message;
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = "حدث خطأ" };
            }
        }
        public List<ShippingOrderLog> GetLog(int ShippingOrderId)
        {
            var shippingOrderLogList = _unitOfWork.ShippingOrderRepository.GetLog(ShippingOrderId);
            return shippingOrderLogList;
        }

        public ReturnResultVM UpdateCarrier(List<int> shippingOrderIds, int carrierId)
        {
            try
            {
                var shippingOrders = _unitOfWork.ShippingOrderRepository.GetAll(x => shippingOrderIds.Contains(x.ID) && !x.IsDeleted).ToList();
                foreach (var shippingOrder in shippingOrders)
                {
                    shippingOrder.CarrierId = carrierId;
                    var result = Update(shippingOrder);
                    if (result.Status != ReturnResultStatusEnum.Success)
                    {
                        result.ErrorMessage = "حدث خطأ اثناء تحديث المندوب";
                        return result;
                    }
                }
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Success};
            }
            catch (Exception ex)
            {
                string Msg = ex.Message;
                _unitOfWork.ShippingOrderRepository.RollbackTransaction();
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = "حدث خطأ اثناء تحديث المندوب" };
            }
        }

        public ReturnResultVM ExtractToCarrierAsExcelFile(List<int> shippingOrderIds, int carrierId)
        {
            try
            {
                var UpdateCarrierResult =  UpdateCarrier(shippingOrderIds, carrierId);
                if (UpdateCarrierResult.Status != ReturnResultStatusEnum.Success)
                    return UpdateCarrierResult;
                var shippingOrders= _unitOfWork.ShippingOrderRepository.Get(shippingOrderIds);
                var shippingOrdersForFile = shippingOrders.Select(x => new ShippingOrderCarrierFileVM()
                {
                    OrderNumber = x.OrderNumber,
                    ClientName = x.ClientName,
                    ClientPhoneNumber = x.ClientPhoneNumber,
                    Governorate = x.Governorate,
                    Address = x.Address,
                    ShipperName = x.Shipper.Name,
                    OrderTotalPrice = x.OrderTotalPrice,
                    DeliveryPrice = x.DeliveryPrice == 0 ? null : x.DeliveryPrice,
                    ShippingPrice = x.ShippingPrice,
                    OrderNetPrice = x.OrderNetPrice,
                    DeliveryStatusName = x.DeliveryStatus.Name,
                    DeliveryStatusReason = x.DeliveryStatusReason,
                    Notes = x.Notes,
                    CarrierName = x.Carrier.Name,
                }).ToList();

                ShippingOrderCarrierFileVM shippingOrdersFileFooter = new ShippingOrderCarrierFileVM() {
                    OrderNumber = null,
                    ClientName = null,
                    ClientPhoneNumber = null,
                    Governorate = null,
                    Address = null,
                    ShipperName = null,
                    OrderTotalPrice = shippingOrders.Sum(x=>x.OrderTotalPrice),
                    DeliveryPrice = 0,
                    ShippingPrice = shippingOrders.Sum(x => x.ShippingPrice),
                    OrderNetPrice = shippingOrders.Sum(x => x.OrderNetPrice),
                    DeliveryStatusName = null,
                    DeliveryStatusReason = null,
                    Notes = null,
                    CarrierName = null,
                };

                shippingOrdersForFile.Add(shippingOrdersFileFooter);

              string FileName = _configuration.GetValue<string>("FilesInfo:ShippingOrderCarrierFile:FileName");
                string fileName = FileName + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx";
                ExcelFileInfo<ShippingOrderCarrierFileVM> shippingOrderList = new ExcelFileInfo<ShippingOrderCarrierFileVM>()
                {
                    WorkbookTitle = _configuration.GetValue<string>("FilesInfo:ShippingOrderCarrierFile:WorkbookTitle"),
                    WorkbookSubject = _configuration.GetValue<string>("FilesInfo:ShippingOrderCarrierFile:WorkbookSubject"),
                    WorkbookAuthor = _configuration.GetValue<string>("FilesInfo:ShippingOrderCarrierFile:WorkbookAuthor"),
                    SheetName = _configuration.GetValue<string>("FilesInfo:ShippingOrderCarrierFile:SheetName"),
                    ColumnNames = new string[] {
                        _configuration.GetValue<string>("FilesInfo:ShippingOrderCarrierFile:columnsNameAR:OrderNumber"),
                        _configuration.GetValue<string>("FilesInfo:ShippingOrderCarrierFile:columnsNameAR:ClientName "),
                        _configuration.GetValue<string>("FilesInfo:ShippingOrderCarrierFile:columnsNameAR:ClientPhoneNumber "),
                        _configuration.GetValue<string>("FilesInfo:ShippingOrderCarrierFile:columnsNameAR:Governorate "),
                        _configuration.GetValue<string>("FilesInfo:ShippingOrderCarrierFile:columnsNameAR:Address "),
                        _configuration.GetValue<string>("FilesInfo:ShippingOrderCarrierFile:columnsNameAR:ShipperName "),
                        _configuration.GetValue<string>("FilesInfo:ShippingOrderCarrierFile:columnsNameAR:OrderTotalPrice "),
                        _configuration.GetValue<string>("FilesInfo:ShippingOrderCarrierFile:columnsNameAR:DeliveryPrice "),
                        _configuration.GetValue<string>("FilesInfo:ShippingOrderCarrierFile:columnsNameAR:ShippingPrice "),
                        _configuration.GetValue<string>("FilesInfo:ShippingOrderCarrierFile:columnsNameAR:OrderNetPrice "),
                        _configuration.GetValue<string>("FilesInfo:ShippingOrderCarrierFile:columnsNameAR:DeliveryStatusName "),
                        _configuration.GetValue<string>("FilesInfo:ShippingOrderCarrierFile:columnsNameAR:DeliveryStatusReason "),
                        _configuration.GetValue<string>("FilesInfo:ShippingOrderCarrierFile:columnsNameAR:Notes "),
                        _configuration.GetValue<string>("FilesInfo:ShippingOrderCarrierFile:columnsNameAR:CarrierName "),
                    },
                    RowDataList = shippingOrdersForFile,
                    SaveFolderPath = FolderPathEnum.ShippingOrderAddFromExcel,
                    FileName = fileName
                };
                _fileManagementService.CreateWellStyled1ExcelFileAndSave(shippingOrderList, true);
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Success, DataObj = fileName };
            }
            catch (Exception ex)
            {
                return new ReturnResultVM() { Status = ReturnResultStatusEnum.Failure, ErrorMessage = "حدث خطأ اثناء تكوين الملف" };
            }
        }

       
    }
}
