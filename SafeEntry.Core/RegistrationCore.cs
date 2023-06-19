using Microsoft.Extensions.DependencyInjection;
using SafeEntry.Core.Interfaces;
using SafeEntry.Core.Services;
using SafeEntry.Core.Services.EmailService;
using System.Reflection;

namespace SafeEntry.Core
{
    public static class RegistrationCore
    {
        public static IServiceCollection RegisterCore(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IPeopleRegistrationEventService, PeopleRegistrationEventService>();
            services.AddScoped<IEventRegistrationService, EventRegistrationService>();
            services.AddScoped<IOrganizerEventsService, OrganizerEventService>();
            services.AddScoped<IOrganizerRegistrationService, OrganizerRegistrationService>();
            services.AddScoped<IUserRegisterService, UserRegisterService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserLoginService, UserLoginService>();

            return services;
        }
    }
}
