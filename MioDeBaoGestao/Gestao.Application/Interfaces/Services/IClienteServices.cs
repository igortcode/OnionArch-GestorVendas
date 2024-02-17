using Gestao.Application.DTO.Cliente;
using Gestao.Application.DTO.Generic;
using System.Threading.Tasks;

namespace Gestao.Application.Interfaces.Services
{
    public interface IClienteServices : IGenericServices<ClienteDTO, MessageDTO> 
    {
        Task<GGet<ObterClienteDTO>> BuscarPorIdAsync(int id);
        Task<GList<ObterClienteDTO>> ListarAsync();
        Task<MessageDTO> ExcluirAsync(int id);
    }
}
