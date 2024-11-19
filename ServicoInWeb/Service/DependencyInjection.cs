using ServicoInWeb.Models;

namespace ServicoInWeb.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection Addinfra(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IHttpBaseModel, HttpBaseModel>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<EmailModel>();
            services.AddSingleton<UrlService>();//adicionar instancia sem uso de interface
            services.Configure<EmailModel>(configuration.GetSection("EmailSettings"));

            return services;
        }
    }
}
