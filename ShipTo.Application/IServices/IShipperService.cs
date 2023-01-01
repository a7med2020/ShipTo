using ShipTo.Core.Entities;
using ShipTo.Core.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Application.IServices
{
    public interface IShipperService
    {
        Shipper Get(int Id);
        List<Shipper> Get();
        ReturnResultVM Add(Shipper shipper);
        ReturnResultVM Update(Shipper shipper);
        ReturnResultVM Delete(int ID);
    }
}
