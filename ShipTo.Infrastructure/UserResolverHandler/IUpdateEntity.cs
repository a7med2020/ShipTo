using System;
using System.Collections.Generic;
using System.Text;

namespace ShipTo.Infrastructure.UserResolverHandler
{
    public interface IUpdateEntity /*: IEntity*/
    {
        string ModefiedBy { get; set; }
        DateTime? ModefiedDate { get; set; }
    }
}
