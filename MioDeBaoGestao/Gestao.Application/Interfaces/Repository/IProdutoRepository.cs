using Gestao.Application.DTO.Generic;
using Gestao.Application.DTO.Produto;
using Gestao.Core.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestao.Application.Interfaces.Repository
{
    public interface IProdutoRepository : IGenericRepository<Produto>
    {
        Task<(IList<ObterProdutoDto>, PagedListMetaDataDTO)> ListarPaginadoAsync(int page, int pageSize);
        Task<(IList<ObterProdutoDto>, PagedListMetaDataDTO)> PesquisarPaginadoAsync(string search, int page, int pageSize);
    }
}
