using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Core.VMs
{
    public class ReturnResultVM
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public Object DataObj { get; set; }
    }
}
