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
        Task<GList<ObterProdutoDto>> ListarPaginadoAsync(int page, int pageSize);
        Task<GList<ObterProdutoDto>> PesquisarPaginadoAsync(string search, int page, int pageSize);
        Task<GList<ObterProdutoDto>> PesquisarProdutoQuantidadePositivaPaginadoAsync(string search, int page, int pageSize);
    }
}
