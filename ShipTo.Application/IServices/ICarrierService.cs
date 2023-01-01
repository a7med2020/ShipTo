using ShipTo.Core.Entities;
using ShipTo.Core.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Application.IServices
{
    public interface ICarrierService
    {
        Carrier Get(int Id);
        List<Carrier> Get();
        ReturnResultVM Add(Carrier carrier);
        ReturnResultVM Update(Carrier carrier);
        ReturnResultVM Delete(int ID);
    }
}
