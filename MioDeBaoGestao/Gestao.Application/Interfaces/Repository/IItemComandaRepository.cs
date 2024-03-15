using Gestao.Application.DTO.Generic;
using Gestao.Application.DTO.ItemComanda;
using Gestao.Core.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestao.Application.Interfaces.Repository
{
    public interface IItemComandaRepository : IGenericRepository<ItemComanda>
    {
        Task<(IList<ObterItemComandaDTO>, PagedListMetaDataDTO)> ListarPaginadoPorIdComandaAsync(int idComanda, int page, int pageSize);
        Task<(IList<ObterItemComandaDTO>, PagedListMetaDataDTO)> PesquisarPaginadoPorIdComandaAsync(string search, int idComanda, int page, int pageSize);
    }
}
