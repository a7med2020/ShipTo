using ShipTo.Core.Entities._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Infrastructure.UserResolverHandler
{
    public abstract class EntityBase2 /*: IEntity*/
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
