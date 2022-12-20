using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Core.Entities
{
    public class ShippingOrderColumnInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        public string DBColumnName { get; set; }
        public string ColumnNameEn { get; set; }
        public string ColumnNameAr { get; set; }
        public bool IsAddByExcel { get; set; }

    }
}
