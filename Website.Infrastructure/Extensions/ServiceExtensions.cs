using Microsoft.Extensions.DependencyInjection;
using Website.Services.Data;
using Website.Services.Data.Interfaces;

namespace Website.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
