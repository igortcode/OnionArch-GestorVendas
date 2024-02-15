using Gestao.Application.DTO.AberturaDia;
using Gestao.Application.DTO.Generic;
using System.Threading.Tasks;

namespace Gestao.Application.Interfaces.Services
{
    public interface IAberturaDiaServices
    {
        Task<MessageDTO> AbrirDiaAsync();
        Task<MessageDTO> FecharDiaAsync();
        Task<GGet<AberturaDiaDTO>> BuscarAberturaDiaPorId(int id);
        Task<GList<AberturaDiaDTO>> ListarAberturasDiasAsync();
    }
}
