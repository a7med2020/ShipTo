using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Infrastructure.UserResolverHandler
{
    public abstract class UpdateEntityBase : EntityBase2, IUpdateEntity
    {
        public string ModefiedBy { get; set; }
        public DateTime? ModefiedDate { get; set; }
    }
}
