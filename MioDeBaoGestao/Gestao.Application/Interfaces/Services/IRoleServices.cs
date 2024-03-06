using Gestao.Application.DTO.Cargos;
using Gestao.Application.DTO.Generic;
using System.Threading.Tasks;

namespace Gestao.Application.Interfaces.Services
{
    public interface IRoleServices
    {
        Task<GList<CargoDTO>> ListarCargos();
    }
}
