using Gestao.Application.DTO.Generic;
using Gestao.Application.DTO.Produto;

namespace Gestao.Application.Interfaces.Services
{
    public interface IProdutoServices : IGenericServices<ProdutoDTO, GGet<ObterProdutoDto>, GList<ObterProdutoDto>, MessageDTO>
    {
    }
}
