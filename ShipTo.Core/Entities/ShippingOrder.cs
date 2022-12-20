using ShipTo.Core.Entities._Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Core.Entities
{
    public class ShippingOrder : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int ID { get; set; }
        [StringLength(30)]
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }

        [Required]
        [StringLength(100)]
        public string ClientName { get; set; }
        [Required]
        [StringLength(20)]
        public string ClientPhoneNumber { get; set; }
        [StringLength(100)]
        public string Direction { get; set; }
        [StringLength(50)]
        public string Governorate { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [ForeignKey("Shipper")]
        public int ShipperId { get; set; }
        [StringLength(300)]
        public string OrderDetails { get; set; }
        public int? OrderPiecesCount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OrderTotalPrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal DeliveryPrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ShippingPrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OrderNetPrice { get; set; }
        [ForeignKey("DeliveryStatus")]
        [Required]
        [StringLength(10)]
        public string DeliveryStatusId { get; set; }
        [StringLength(300)]
        public string DeliveryStatusReason { get; set; }
        [ForeignKey("Carrier")]
        public int CarrierId { get; set; }
        [StringLength(150)]
        public string FileDataName { get; set; }

        public Shipper Shipper { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public Carrier Carrier { get; set; }
    }
}
