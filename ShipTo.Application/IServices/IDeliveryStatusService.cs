﻿using ShipTo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Application.IServices
{
    public interface IDeliveryStatusService
    {
        List<DeliveryStatus> Get();
    }
}
