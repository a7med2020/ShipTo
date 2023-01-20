using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Core.VMs
{
    public class ExtractToCarrierVM
    {
        [Display(Name = "المندوب")]
        [ForeignKey("Carrier")]
        [Required(ErrorMessage = "يجب اختيار المندوب")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب اختيار المندوب")]
        public int Carrier_Id { get; set; }

        public List<int> ShippingOrderIds { get; set; }
    }
}
