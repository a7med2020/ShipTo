using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Reporting.VMs
{
    public class ShippingOrderCarrierFileVM
    {
        public string CompanyName { get; set; }
        public string CompanyLogo { get; set; }
        public string OrderNumber { get; set; }
        public string ClientName { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string Governorate { get; set; }
        public string Address { get; set; }
        public string ShipperName { get; set; }
        public string OrderDetails { get; set; }
        public decimal OrderTotalPrice { get; set; }
        public decimal? DeliveryPrice { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal OrderNetPrice { get; set; }
        public string DeliveryStatusName { get; set; }
        public string DeliveryStatusReason { get; set; }
        public string Notes { get; set; }
        public string CarrierName { get; set; }
    }
}
