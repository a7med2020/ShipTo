﻿using Microsoft.AspNetCore.Identity;
using ShipTo.Core.Entities._Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Core.Entities
{
    public class ShippingOrderLog  
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int ID { get; set; }
        [ForeignKey("ShippingOrder")]

        public int ShippingOrderID { get; set; }
        [Display(Name = "رقم الطلب")]
        public string OrderNumber { get; set; }
        [Display(Name = "رقم المجموعة")]
        [StringLength(100)]
        public string BulkId { get; set; }
        [Display(Name = "تاريخ الطلب")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "الاسم المجمع")]
        [StringLength(500)]
        public string ShippingOrderBulkName { get; set; }
        [Display(Name = "العميل")]
        [Required]
        [StringLength(200)]
        public string ClientName { get; set; }
        [Display(Name = "هاتف العميل")]
        [Required]
        [StringLength(100)]
        public string ClientPhoneNumber { get; set; }
        [Display(Name = "الجهه")]
        [StringLength(100)]
        public string Direction { get; set; }
        [Display(Name = "المحافظه")]
        [StringLength(100)]
        public string Governorate { get; set; }
        [Display(Name = " اسم العميل")]
        [StringLength(250)]
        public string Address { get; set; }
        [Display(Name = "شركة الشحن")]
        [ForeignKey("Shipper")]
        public int ShipperId { get; set; }
        [Display(Name = "تفاصيل الطلب")]
        [StringLength(300)]
        public string OrderDetails { get; set; }
        [Display(Name = "عدد القطع")]
        public int? OrderPiecesCount { get; set; }
        [Display(Name = "سعر الطلب")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OrderTotalPrice { get; set; }
        [Display(Name = "سعر التسليم")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal DeliveryPrice { get; set; }
        [Display(Name = "سعر الشحن")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ShippingPrice { get; set; }
        [Display(Name = "سعر المنتج")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OrderNetPrice { get; set; }
        [Display(Name = "حالة التسليم")]
        [ForeignKey("DeliveryStatus")]
        [Required]
        [StringLength(20)]
        public string DeliveryStatusId { get; set; }
        [Display(Name = "سبب حالة التسليم")]
        [StringLength(300)]
        public string DeliveryStatusReason { get; set; }
        [Display(Name = "تاريخ التسليم")]
        public DateTime? DeliveryDate { get; set; }
        [Display(Name = "المندوب")]
        [ForeignKey("Carrier")]
        public int? CarrierId { get; set; }
        [Display(Name = "اسم فيل البيانات")]
        [StringLength(150)]
        public string FileDataName { get; set; }
        [IgnoreDataMember]
        public ShippingOrder ShippingOrder { get; set; }
        public Shipper Shipper { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public Carrier Carrier { get; set; }


        [Display(Name = "إضيف بواسطة")]
        [ForeignKey("CreatedByUser")]
        public string CreatedBy { get; set; }
        [Display(Name = "عدل بواسطة")]
        [ForeignKey("ModefiedByUser")]
        public string ModefiedBy { get; set; }
        [Display(Name = "تاريخ الإضافة")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "تاريخ التعديل")]
        [ScaffoldColumn(false)]
        public DateTime? ModefiedDate { get; set; }
        [Display(Name = "حذف؟")]
        [ScaffoldColumn(false)]
        public bool IsDeleted { get; set; }
        [Display(Name = "تاريخ الحذف")]
        [ScaffoldColumn(false)]
        public DateTime? DeletedDate { get; set; }
        [Display(Name = "حذف بواسطة")]
        [ScaffoldColumn(false)]
        [ForeignKey("DeletedByUser")]
        public string DeletedBy { get; set; }
        [ScaffoldColumn(false)]
        [Display(Name = "ملاحظات")]
        [StringLength(500)]
        public string Notes { get; set; }
        
        public IdentityUser CreatedByUser { get; set; }
        
        public IdentityUser ModefiedByUser { get; set; }
        public IdentityUser DeletedByUser { get; set; }

    }
}
