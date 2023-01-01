using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Core.Enums
{
    public class DeliveryStatusEnum
    {
        public static string UnderDelivery { get { return "UnderDelivery"; } }

        public static string Delivered { get { return "Delivered"; } }
        public static string PartialDelivery { get { return "PartialDelivery"; } }
        public static string Refused { get { return "Refused"; } }
    }
}
