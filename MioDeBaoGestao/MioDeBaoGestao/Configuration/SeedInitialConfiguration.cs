using Gestao.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MioDeBaoGestao.Configuration
{
    public static class SeedInitialConfiguration
    {
        public static IServiceCollection SeedInitial(this IServiceCollection services)
        {

            using (var provider = services.BuildServiceProvider())
            {
                var seedServices = provider.GetRequiredService<ISeedServices>();

                seedServices.AplayMigrations();

                seedServices.SeedRoles();

                seedServices.SeedAdminUser();
            }


            return services;
        }
    }
}
