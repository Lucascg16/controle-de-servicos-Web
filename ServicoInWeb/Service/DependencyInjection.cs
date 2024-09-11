using ServicoInWeb.Models;

namespace ServicoInWeb.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection Addinfra(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<IHttpBaseModel, HttpBaseModel>();

            return services;
        }
    }
}
