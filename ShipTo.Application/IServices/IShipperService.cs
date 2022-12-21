using ShipTo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Application.IServices
{
    public interface IShipperService
    {
        List<Shipper> Get();
        string Add(Shipper shipper);
        string Update(Shipper shipper);
        string Delete(Shipper shipper);
    }
}
