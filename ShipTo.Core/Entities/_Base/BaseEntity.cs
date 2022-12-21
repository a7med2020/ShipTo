using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Core.Entities._Base
{
    public class BaseEntity
    {
        [ScaffoldColumn(false)]
        public bool IsDeleted { get; set; }
         
        [ForeignKey("CreatedByUser")]
        [Display(Name = "إضيف بواسطة")]
        public string CreatedBy { get; set; }
        [Display(Name = "عدل بواسطة")]
        [ForeignKey("ModefiedByUser")]
        public string ModefiedBy { get; set; }
        [Display(Name = "تاريخ الإضافة")]
        public DateTime CreatedDate { get; set; }
        [ScaffoldColumn(false)]
        [Display(Name = "تاريخ التعديل")]
        public DateTime? ModefiedDate { get; set; }
        [ScaffoldColumn(false)]
        [Display(Name = "ملاحظات")]
        [StringLength(500)]
        public string Notes { get; set; }
        [ScaffoldColumn(false)]
        public IdentityUser CreatedByUser { get; set; }
        [ScaffoldColumn(false)]
        public IdentityUser ModefiedByUser { get; set; }


    }
}
