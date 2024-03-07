using Microsoft.EntityFrameworkCore.Migrations;
using SuperMarket.Business.Interfaces;
using SuperMarket.Business.Repositories;
using SuperMarket.Web.Services;

namespace SuperMarket.Web.Configurations
{
    public static class ConfigurationBusinessService
    {
        public static IServiceCollection AddBusinessService(this IServiceCollection service)
        {
            //Repository
            service.AddScoped<IWareHouseRepository, WareHouseRepository>();
            service.AddScoped<IProductRepository, ProductRepository>();

            //service
            service.AddScoped<WareHouseService>();
            service.AddScoped<ProductService>();


            return service;
        }
    }
}
