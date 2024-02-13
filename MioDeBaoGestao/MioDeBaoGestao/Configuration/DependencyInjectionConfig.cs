using Gestao.Core.Interfaces.Facade;
using Gestao.Core.Interfaces.Repository;
using Gestao.Core.Interfaces.Services;
using Gestao.Core.Services;
using Gestao.Data.Identity.Services;
using Gestao.Data.Repository;
using Gestao.Notification.Notification.Facade;
using Microsoft.Extensions.DependencyInjection;
using MioDeBaoGestao.Repository;

namespace MioDeBaoGestao.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencyInjection(this IServiceCollection services)
        {
            #region Services
            services.AddScoped<ISeedServices, SeedServices>();
            services.AddScoped<IProdutoServices, ProdutoServices>();
            services.AddScoped<IAberturaDiaServices, AberturaDiaServices>();
            #endregion

            #region Facade
            services.AddScoped<IEmailFacade, EmailFacade>();

            #endregion

            #region Repository
            services.AddScoped<IAberturaDiaRepository, AberturaDiaRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IComandaRepository, ComandaRepository>();
            services.AddScoped<IItemComandaRepository, ItemComandaRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            #endregion


            return services;
        }
    }
}
