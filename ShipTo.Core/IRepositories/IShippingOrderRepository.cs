﻿using ShipTo.Core.Entities;
using ShipTo.Core.IRepositories._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Core.IRepositories
{
    public interface IShippingOrderRepository : IBaseRepository<ShippingOrder>
    {

        void Log(ShippingOrder shippingOrder);
    }
}
