﻿using Microsoft.Extensions.DependencyInjection;
using ShipTo.Application.IServices;
using ShipTo.Application.Services;

namespace ShipTo.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection serviceCollection)
        {
             serviceCollection.AddScoped<IShippingOrderService, ShippingOrderService>();
             serviceCollection.AddScoped<IShipperService, ShipperService>();
             serviceCollection.AddScoped<IDeliveryStatusService, DeliveryStatusService>();
             serviceCollection.AddScoped<ICarrierService, CarrierService>();
             serviceCollection.AddScoped<IShippingOrderColumnInfoService, ShippingOrderColumnInfoService>();
             serviceCollection.AddScoped<IFileManagementService, FileManagementService>();
        }
    }
}
