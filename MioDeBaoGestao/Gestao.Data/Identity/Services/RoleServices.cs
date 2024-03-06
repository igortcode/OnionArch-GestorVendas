using Gestao.Application.DTO.Cargos;
using Gestao.Application.DTO.Generic;
using Gestao.Application.Enums;
using Gestao.Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gestao.Data.Identity.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleServices(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<GList<CargoDTO>> ListarCargos()
        {
            try
            {
                var roles = await _roleManager.Roles.Select(a => new CargoDTO { Role = a.Name}).ToListAsync();


                return new GList<CargoDTO>
                {
                    DTOs = roles,
                    Message = new MessageDTO("Busca efetuada com sucesso!")
                };
            }
            catch (Exception ex)
            {
                return new GList<CargoDTO> { Message = new MessageDTO("Erro ao efetuar a busca", TipoNotificacao.Erro, ex) };
            }
        }
    }
}
