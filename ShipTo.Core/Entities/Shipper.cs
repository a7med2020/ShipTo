
using ShipTo.Core.Entities._Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace ShipTo.Core.Entities
{
    
    public class Shipper : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int ID { get; set; }
        
        [Display(Name="الاسم")]
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Display(Name = "رقم الهاتف")]
        [StringLength(20)]
        public string Phone { get; set; }
        [Display(Name = "العنوان")]
        [StringLength(100)]
        public string Address { get; set; }
        [Display(Name = "الإميل")]
        [StringLength(100)]
        public string Email { get; set; }
        public IList<ShippingOrder> ShippingOrders { get; } = new List<ShippingOrder>();
    }
}
