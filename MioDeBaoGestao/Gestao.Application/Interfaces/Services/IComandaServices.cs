using Gestao.Application.DTO.Comanda;
using Gestao.Application.DTO.Generic;
using System.Threading.Tasks;

namespace Gestao.Application.Interfaces.Services
{
    public interface IComandaServices : IGenericServices<ComandaDTO, GGet<ObterComandaDTO>, GList<ObterComandaDTO>, MessageDTO>
    {
        Task<MessageDTO> FecharComanda(int id, int idAberturaDia);
        Task<GGet<ObterComandaDTO>> BuscarPorIdEAberturadiaAsync(int id, int idAberturaDia);
    }
}
