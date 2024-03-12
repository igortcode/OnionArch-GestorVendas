using Gestao.Application.DTO.Comanda;
using Gestao.Application.DTO.Generic;
using Gestao.Core.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestao.Application.Interfaces.Repository
{
    public interface IComandaRepository : IGenericRepository<Comanda>
    {
        Task<(IList<ObterComandaDTO>, PagedListMetaDataDTO)> PesquisarPaginadoAsync(int aberturaDiaId, string search, int page, int pageSize);
        Task<(IList<ObterComandaDTO>, PagedListMetaDataDTO)> ListarPaginadoAsync(int aberturaDiaId, int page, int pageSize);
    }
}
