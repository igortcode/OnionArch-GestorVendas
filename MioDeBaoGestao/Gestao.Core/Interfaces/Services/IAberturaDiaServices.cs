using Gestao.Core.DTO.AberturaDia;
using Gestao.Core.DTO.Generic;
using System.Threading.Tasks;

namespace Gestao.Core.Interfaces.Services
{
    public interface IAberturaDiaServices
    {
        Task<MessageDTO> AbrirDiaAsync();
        Task<MessageDTO> FecharDiaAsync();
        Task<GGet<AberturaDiaDTO>> BuscarAberturaDiaPorId(int id);
        Task<GList<AberturaDiaDTO>> ListarAberturasDiasAsync();
    }
}
