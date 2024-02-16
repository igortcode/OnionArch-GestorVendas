using System.Threading.Tasks;

namespace Gestao.Application.Interfaces.Services
{
    public interface IGenericServices<TDTO, GGet, GList, MessDto>
    {
        Task<MessDto> AdicionarAsync(TDTO entity);
        Task<MessDto> AtualizarAsync(int id, TDTO entity);
        Task<GGet> BuscarPorIdAsync(int id);
        Task<GList> ListarAsync();
        Task<MessDto> ExcluirAsync(int id);
    }
}
