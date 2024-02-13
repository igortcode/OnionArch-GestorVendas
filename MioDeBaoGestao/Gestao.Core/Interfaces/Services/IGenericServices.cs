using Gestao.Core.DTO.Generic;
using System.Threading.Tasks;

namespace Gestao.Core.Interfaces.Services
{
    public interface IGenericServices<TDTO, GGet, GList, MessDto>
    {
        Task<MessDto> AdicionarAsync(TDTO entity);
        Task<MessDto> AtualizarAsync(int id, TDTO entity);
        Task<GGet> BuscarPorIdAsync(int id);
        Task<GList> ListarAsync();
        Task<GList> PesquisarAsync(string pesquisa);
        Task<MessDto> DesabilitarAsync(int id);
        Task<MessDto> ExcluirAsync(int id);
    }
}
