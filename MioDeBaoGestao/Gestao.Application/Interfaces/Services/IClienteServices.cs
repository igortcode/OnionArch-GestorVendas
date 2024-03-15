using Gestao.Application.DTO.Cliente;
using Gestao.Application.DTO.Generic;
using System.Threading.Tasks;

namespace Gestao.Application.Interfaces.Services
{
    public interface IClienteServices : IGenericServices<ClienteDTO, MessageDTO> 
    {
        Task<GGet<ObterClienteDTO>> BuscarPorIdAsync(int id);
        Task<GList<ObterClienteDTO>> ListarAsync();
        Task<GList<ObterClienteDTO>> ListarPaginadoAsync(int page, int pageSize);
        Task<GList<ObterClienteDTO>> PesquisarPaginadoAsync(string search, int page, int pageSize);
        Task<MessageDTO> ExcluirAsync(int id);
    }
}
