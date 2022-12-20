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
       
        public bool IsDeleted { get; set; }
        [ForeignKey("CreatedByUser")]
        public string CreatedBy { get; set; }
        [ForeignKey("ModefiedByUser")]
        public string ModefiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModefiedDate { get; set; }
        [StringLength(500)]
        public string Notes { get; set; }

        public IdentityUser CreatedByUser { get; set; }
        public IdentityUser ModefiedByUser { get; set; }


    }
}
