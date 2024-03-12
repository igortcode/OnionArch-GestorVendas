using Gestao.Application.DTO.Generic;
using Gestao.Application.DTO.ItemComanda;
using System.Threading.Tasks;

namespace Gestao.Application.Interfaces.Services
{
    public interface IItemComandaServices : IGenericServices<ItemComandaDTO, MessageDTO>
    {
        Task<GGet<ObterItemComandaDTO>> ObterItemComandaPorIdEIdComanda(int id, int idComanda);
        Task<MessageDTO> ExcluirItemComandaPorIdEIdComanda(int id, int idComanda, int quantidade);
        Task<GList<ObterItemComandaDTO>> ListarItemComandaPorIdEIdComanda(int idComanda);
        Task<GList<ObterItemComandaDTO>> ListarItemComandaPorIdEIdComandaPaginadoAsync(int idComanda, int page, int pageSize);
        Task<GList<ObterItemComandaDTO>> PesquisarItemComandaPorIdEIdComandaPaginadoAsync(string search, int idComanda, int page, int pageSize);
    }
}
