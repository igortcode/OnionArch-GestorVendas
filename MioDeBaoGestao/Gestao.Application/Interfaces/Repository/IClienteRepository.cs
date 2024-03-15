using Gestao.Application.DTO.Cliente;
using Gestao.Application.DTO.Generic;
using Gestao.Core.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestao.Application.Interfaces.Repository
{
    public interface IClienteRepository : IGenericRepository<Cliente>
    {
        Task<(IList<ObterClienteDTO>, PagedListMetaDataDTO)> ListarPaginadoAsync(int page, int pageSize);
        Task<(IList<ObterClienteDTO>, PagedListMetaDataDTO)> PesquisarPaginadoAsync(string search, int page, int pageSize);
    }
}
