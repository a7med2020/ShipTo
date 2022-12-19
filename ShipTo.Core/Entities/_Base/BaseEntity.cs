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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int ID { get; set; }

        public int CompanyID { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public int? ModefiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModefiedDate { get; set; }
        public string Notes { get; set; }
    }
}
