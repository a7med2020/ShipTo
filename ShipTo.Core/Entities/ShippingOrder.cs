using ShipTo.Core.Entities._Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShipTo.Core.Entities
{
    public class ShippingOrder : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int ID { get; set; }
        [Display(Name = "رقم الطلب")]
        [StringLength(30)]
        [Required]
        public string OrderNumber { get; set; }
        [StringLength(100)]
        [Display(Name = "رقم المجموعة")]
        public string BulkId { get; set; }
        [Display(Name = "تاريخ الطلب")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime OrderDate { get; set; }
        [StringLength(500)]
        [Display(Name = "الاسم المجمع")]
        public string ShippingOrderBulkName { get; set; }
        [Display(Name = " اسم العميل")]
        [Required(ErrorMessage = "يجب إدخال العميل")]
        [StringLength(200)]
        public string ClientName { get; set; }
        [Display(Name = "هاتف العميل")]
        [Required(ErrorMessage = "يجب إدخال هاتف العميل")]
        [StringLength(100)]
        public string ClientPhoneNumber { get; set; }
        [Display(Name = "الجهه")]
        [StringLength(100)]
        public string Direction { get; set; }
        [Display(Name = "المحافظه")]
        [StringLength(100)]
        public string Governorate { get; set; }
        [StringLength(250)]
        [Display(Name = "العنوان العميل")]
        [Required(ErrorMessage = "يجب إدخال العنوان")]
        public string Address { get; set; }
        
        [Display(Name = "شركة الشحن")]
        [ForeignKey("Shipper")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب إدخال شركة الشحن")]
        [Required(ErrorMessage = "يجب إدخال شركة الشحن")]
        public int ShipperId { get; set; }
        [Display(Name = "تفاصيل الطلب")]
        [StringLength(300)]
        [Required(ErrorMessage = "يجب إدخال تفاصيل الطلب")]
        public string OrderDetails { get; set; }
        [Display(Name = "عدد القطع")]
        public int? OrderPiecesCount { get; set; }
        [Display(Name = "سعر الطلب")]
        [Column(TypeName = "decimal(18, 2)")]
        [Required(ErrorMessage = "يجب إدخال سعر الطلب")]
        [RegularExpression(@"^\d+(\.\d{1,3})?$", ErrorMessage = "سعر الطلب يجب أن يكون رقم أكبر من او يساوي 1")]
        [Range(1, 9999999999999999.99, ErrorMessage = "سعر الطلب يجب أن يكون رقم أكبر من او يساوي 1")]
        public decimal OrderTotalPrice { get; set; }
        [Display(Name = "سعر التسليم")]
        [Column(TypeName = "decimal(18, 2)")]
        [Required(ErrorMessage = "يجب إدخال سعر التسليم")]
        [RegularExpression(@"^\d+(\.\d{1,3})?$", ErrorMessage = "سعر التسليم يجب أن يكون رقم أكبر من او يساوي 1")]
        [Range(0, 9999999999999999.99, ErrorMessage = "سعر التسليم يجب أن يكون رقم أكبر من او يساوي 0")]
        public decimal DeliveryPrice { get; set; }
        [Display(Name = "سعر الشحن")]
        [Column(TypeName = "decimal(18, 2)")]
        [Required(ErrorMessage = "يجب إدخال سعر المنتج")]
        [RegularExpression(@"^\d+(\.\d{1,3})?$", ErrorMessage = "سعر الشحن يجب أن يكون رقم أكبر من او يساوي 1")]
        [Range(1, 9999999999999999.99, ErrorMessage = "سعر الشحن يجب أن يكون رقم أكبر من او يساوي 1")]
        public decimal ShippingPrice { get; set; }
        [Display(Name = "سعر المنتج")]
        [Column(TypeName = "decimal(18, 2)")]
        [Required(ErrorMessage = "يجب إدخال سعر المنتج")]
        [RegularExpression(@"^\d+(\.\d{1,3})?$", ErrorMessage =  "سعر المنتج يجب أن يكون رقم أكبر من او يساوي 1")]
        [Range(1, 9999999999999999.99, ErrorMessage = "سعر المنتج يجب أن يكون رقم أكبر من او يساوي 1")]
        public decimal OrderNetPrice { get; set; }
        [Display(Name = "حالة التسليم")]
        [ForeignKey("DeliveryStatus")]
        [StringLength(20)]
        [RegularExpression("UnderDelivery|Delivered|PartialDelivery|Refused", ErrorMessage = "يجب إدخال حالة التسليم")]
        [Required(ErrorMessage = "يجب إدخال حالة التسليم")]
        public string DeliveryStatusId { get; set; }
        [Display(Name = "سبب حالة التسليم")]
        [StringLength(300)]
        public string DeliveryStatusReason { get; set; }
        [Display(Name = "تاريخ التسليم")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DeliveryDate { get; set; }
        [Display(Name = "المندوب")]
        [ForeignKey("Carrier")]
        //[Range(1, int.MaxValue, ErrorMessage = "يجب إدخال المندوب")]
        public int? CarrierId { get; set; }
        [Display(Name = "اسم فيل البيانات")]
        [StringLength(150)]
        public string FileDataName { get; set; }
        [Display(Name = "شركة الشحن")]
        public Shipper Shipper { get; set; }
        [Display(Name = "حالة التسليم")]
        public DeliveryStatus DeliveryStatus { get; set; }
        [Display(Name = "المندوب")]
        public Carrier Carrier { get; set; }
        [JsonIgnore]
        
        public IList<ShippingOrderLog> ShippingOrderLogs { get; } = new List<ShippingOrderLog>();

    }
}
