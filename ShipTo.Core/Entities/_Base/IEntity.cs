using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShipTo.Core.Entities._Base
{
    public interface IEntity
    {
        string CreatedBy { get; set; }
        string ModefiedBy { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? ModefiedDate { get; set; }
        bool IsDeleted { get; set; }
        DateTime? DeletedDate { get; set; }
        string DeletedBy { get; set; }
        string Notes { get; set; }
        IdentityUser CreatedByUser { get; set; }
        IdentityUser ModefiedByUser { get; set; }
    }
}
