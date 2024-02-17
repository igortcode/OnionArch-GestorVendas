using Gestao.Application.DTO.Generic;
using Gestao.Application.DTO.Produto;
using System.Threading.Tasks;

namespace Gestao.Application.Interfaces.Services
{
    public interface IProdutoServices : IGenericServices<ProdutoDTO, MessageDTO>
    {
        Task<GGet<ObterProdutoDto>> BuscarPorIdAsync(int id);
        Task<MessageDTO> ExcluirAsync(int id);

        Task<GList<ObterProdutoDto>> ListarAsync();
    }
}
