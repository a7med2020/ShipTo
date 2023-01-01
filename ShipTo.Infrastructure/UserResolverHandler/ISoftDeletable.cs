using System;
using System.Collections.Generic;
using System.Text;

namespace ShipTo.Infrastructure.UserResolverHandler
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedDate { get; set; }
    }
}
