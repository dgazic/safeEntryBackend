using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SafeEntry.Persistance.Context;
using SafeEntry.Persistance.Interfaces;
using SafeEntry.Persistance.Persistance;

namespace SafeEntry.Persistance
{
    public static class RegistrationPersistance
    {
        public static IServiceCollection RegisterPersistance(this IServiceCollection services)
        {
            services.AddSingleton<DapperContext>();
            services.AddScoped<IEventPersistance, EventPersistance>();
            services.AddScoped<IOrganizerPersistance, OrganizerPersistance>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services; 
        }
    }
}
