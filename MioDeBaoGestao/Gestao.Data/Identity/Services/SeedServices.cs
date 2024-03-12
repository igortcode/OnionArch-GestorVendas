using Gestao.Application.Interfaces.Services;
using Gestao.Data.Context;
using Gestao.Data.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;

namespace Gestao.Data.Identity.Services
{
    public class SeedServices : ISeedServices
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationContext _context;

        public SeedServices(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public void AplayMigrations()
        {
            _context.Database.Migrate();
        }

        public void SeedAdminUser()
        {
            if (_context.Database.CanConnect())
            {
                if(!_userManager.Users.Any(a => a.UserName.Equals("admin@miodebao.com")))
                {
                    var user = new User ( userName: "admin@miodebao.com", email: "admin@miodebao.com");

                    _userManager.CreateAsync(user, "MioDeBao@2024").Wait();

                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

        public void SeedRoles()
        {
            if (_context.Database.CanConnect())
            {
                if (!_roleManager.Roles.Any(a => a.Name == "Admin"))
                {
                    _roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
                }

                if (!_roleManager.Roles.Any(a => a.Name == "Vendedor"))
                {
                    _roleManager.CreateAsync(new IdentityRole("Vendedor")).Wait();
                }

                if (!_roleManager.Roles.Any(a => a.Name == "Gerente"))
                {
                    _roleManager.CreateAsync(new IdentityRole("Gerente")).Wait();
                }

            }
        }
    }
}
