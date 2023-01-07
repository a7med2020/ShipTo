using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShipTo.Core.Entities
{
    public class DeliveryStatus
    {
        [StringLength(20)]
        [Key, Column(Order = 0)]
        public string ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [IgnoreDataMember]
        public IList<ShippingOrder> ShippingOrders { get; }

    }
}
