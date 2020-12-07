using API.Business.Interfaces;
using API.Business.Services;
using API.Data.Context;
using API.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace API_Excel_Import.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();
            services.AddScoped<IExcelDataRepository, ExcelDataRepository>();
            services.AddScoped<ILoteRepository, LoteRepository>();

            services.AddScoped<IExcelDataService, ExcelDataService>();
            services.AddScoped<ILoteService, LoteService>();
            return services;
        }
    }
}
