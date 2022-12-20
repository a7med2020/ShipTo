using ShipTo.Core.Entities;
using ShipTo.Core.IRepositories;
using ShipTo.Infrastructure.Contexts;
using ShipTo.Infrastructure.Repositories._Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Infrastructure.Repositories
{
    public class ShippingOrderRepository : BaseRepository<ShippingOrder>, IShippingOrderRepository
    {
        private readonly ShipToContext _context;

        public ShippingOrderRepository(ShipToContext context) : base(context)
        {
            _context = context;
        }
    }
}
