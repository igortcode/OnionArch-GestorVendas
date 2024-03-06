using Gestao.Data.Context;
using Gestao.Data.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MioDeBaoGestao.Profiles;
using System;

namespace MioDeBaoGestao.Configuration
{
    public static class ContextConfigurations
    {
        public static IServiceCollection AddContextConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 34));

            services.AddDbContext<ApplicationContext>(a => 
                        a.UseMySql(configuration.GetConnectionString("MysqlConnection"), serverVersion, 
                        a => a.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

            services.AddIdentity<User, IdentityRole>(options => { 
                options.SignIn.RequireConfirmedAccount = false; 
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase  = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<ApplicationContext>();

            services.ConfigureApplicationCookie(options => {
                options.AccessDeniedPath = "/Login/AccessDenied";
            });


            services.AddAutoMapper(typeof(ProfilesDTO_Entity), typeof(ProfilesDTO_ViewModel));


            return services;
        }
    }
}
