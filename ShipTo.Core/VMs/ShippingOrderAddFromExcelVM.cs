using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Core.VMs
{
    public class ShippingOrderAddFromExcelVM
    {
        [Required(ErrorMessage = "إختيار ملف طلبات الشحن")]
        public object ShippingExcelFile { get; set; }
        public int ID { get; set; }
        [Display(Name = "رقم الطلب")]
        public string OrderNumber { get; set; }
        [Display(Name = "رقم المجموعة")]
        public string BulkId { get; set; }
        [Display(Name = "اسم مجمع")]
        public string ShippingOrderBulkName { get; set; }
        [Display(Name = "تاريخ الطلب")]
        [Required(ErrorMessage = "يجب إدخال تاريخ الطلب")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "العميل")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب إدخال العميل")]
        [Required(ErrorMessage = "يجب إدخال العميل")]
        public string ClientNameColName { get; set; }
        [Display(Name = "هاتف العميل")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب إدخال هاتف العميل")]
        [Required(ErrorMessage = "يجب إدخال هاتف العميل")]
        public string ClientPhoneNumberColName { get; set; }
        [Display(Name = "الجهه")]
        public string DirectionColName { get; set; }
        [Display(Name = "المحافظه")]
        public string GovernorateColName { get; set; }
        [Display(Name = "العنوان")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب إدخال العنوان")]
        [Required(ErrorMessage = "يجب إدخال العنوان")]
        public string AddressColName { get; set; }
        [Display(Name = "شركة الشحن")]
        [Range(1, int.MaxValue,  ErrorMessage = "يجب إدخال شركة الشحن")]
        [Required(ErrorMessage = "يجب إدخال شركة الشحن")]
        public int ShipperId { get; set; }
        [Display(Name = "تفاصيل الطلب")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب إدخال تفاصيل الطلب")]
        [Required(ErrorMessage = "يجب إدخال تفاصيل الطلب")]
        public string OrderDetailsColName { get; set; }
        [Display(Name = "عدد القطع")]
        public int? OrderPiecesCountColName { get; set; }
        [Display(Name = "سعر الطلب")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب إدخال سعر الطلب")]
        [Required(ErrorMessage = "يجب إدخال سعر الطلب")]
        public decimal OrderTotalPriceColName { get; set; }
        [Display(Name = "سعر التسليم")]
        public decimal DeliveryPriceColName { get; set; }
        [Display(Name = "سعر الشحن")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب إدخال سعر الشحن")]
        [Required(ErrorMessage = "يجب إدخال سعر الشحن")]
        public decimal ShippingPriceColName { get; set; }
        [Display(Name = "سعر المنتج")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب إدخال سعر المنتج")]
        [Required(ErrorMessage = "يجب إدخال سعر المنتج")]
        public decimal OrderNetPriceColName { get; set; }
        [Display(Name = "حالة التسليم")]
        public string DeliveryStatusId { get; set; }
        [Display(Name = "سبب حالة التسليم")]
        public string DeliveryStatusReasonColName { get; set; }
        [Display(Name = "تاريخ التسليم")]
        [Required(ErrorMessage = "يجب إدخال تاريخ التسليم")]
        public DateTime DeliveryDate { get; set; }
        [Display(Name = "المندوب")]
        public int? CarrierId { get; set; }
        [Display(Name = "اسم فيل البيانات")]
        public string FileDataName { get; set; }
        [Display(Name = "ملاحظات")]
        [StringLength(500)]
        public string NotesColName { get; set; }
    }
}
