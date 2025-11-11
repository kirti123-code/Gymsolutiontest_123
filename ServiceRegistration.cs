using SERVICES.Interfaces;
using SERVICES.Repository;
using GymCraftSolutionsWebAPI.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace SERVICES.Common
{
    public static class ServiceRegistration
    {
        public static void AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserDetailsService, UserDetailsService>();
            services.AddScoped<IRolesService, RoleService>();
            services.AddScoped<BusBookingService, BusBookingService>();
        }
    }
}