using System.Threading.Tasks;

namespace Gestao.Application.Interfaces.Services
{
    public interface IGenericServices<TDTO, MessDto>
    {
        Task<MessDto> AdicionarAsync(TDTO entity);
        Task<MessDto> AtualizarAsync(int id, TDTO entity);
    }
}
