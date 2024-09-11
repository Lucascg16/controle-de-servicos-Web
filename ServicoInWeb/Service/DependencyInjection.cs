using ServicoInWeb.Models;

namespace ServicoInWeb.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection Addinfra(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<IHttpBaseModel, HttpBaseModel>();

            return services;
        }
    }
}
