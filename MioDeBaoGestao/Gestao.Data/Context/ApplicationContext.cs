using Gestao.Core.Entidades;
using Gestao.Data.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Gestao.Data.Context
{
    public class ApplicationContext : IdentityDbContext<User>
    {

        public DbSet<AberturaDia> AberturaDias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Comanda> Comandas { get; set; }
        public DbSet<ItemComanda> ItensComanda { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options) 
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

    }
}
