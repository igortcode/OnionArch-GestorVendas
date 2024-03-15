using Gestao.Application.DTO.Comanda;
using Gestao.Application.DTO.Generic;
using System.Threading.Tasks;

namespace Gestao.Application.Interfaces.Services
{
    public interface IComandaServices : IGenericServices<ComandaDTO, MessageDTO>
    {
        Task<MessageDTO> FecharComanda(int id, int idAberturaDia);
        Task<GGet<ObterComandaDTO>> BuscarPorIdEAberturadiaAsync(int id, int idAberturaDia);
        Task<GGet<ObterComandaDTO>> BuscarPorIdAsync(int id);
        GGet<int> BuscarUltimoIdRegistrado();
        Task<GList<ObterComandaDTO>> ListarPorAberturaDiaAsync(int idAberturaDia);
        Task<GList<ObterComandaDTO>> ListarPaginadoPorAberturaDiaAsync(int idAberturaDia, int page, int pageSize);
        Task<GList<ObterComandaDTO>> PesquisarPaginadoPorAberturaDiaAsync(int idAberturaDia, string search, int page, int pageSize);
        Task<MessageDTO> ExcluirAsync(int id, int idAberturaDia);
    }
}
