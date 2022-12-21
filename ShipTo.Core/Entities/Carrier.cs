using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Core.Entities
{
    public class Carrier
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int ID { get; set; }

        [Display(Name = "الاسم")]
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
    }
}
