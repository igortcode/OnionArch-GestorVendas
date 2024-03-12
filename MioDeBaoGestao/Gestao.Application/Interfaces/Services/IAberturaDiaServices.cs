using Gestao.Application.DTO.AberturaDia;
using Gestao.Application.DTO.Generic;
using Gestao.Application.DTO.RelatorioDTO;
using System.Threading.Tasks;

namespace Gestao.Application.Interfaces.Services
{
    public interface IAberturaDiaServices
    {
        Task<MessageDTO> AbrirDiaAsync();
        Task<MessageDTO> FecharDiaAsync(int id);
        Task<GGet<AberturaDiaDTO>> BuscarAberturaDiaPorId(int id);
        Task<GList<AberturaDiaDTO>> ListarAberturasDiasAsync();
        Task<GList<RelatorioVendasDTO>> RelatorioVendasAsync();
    }
}
