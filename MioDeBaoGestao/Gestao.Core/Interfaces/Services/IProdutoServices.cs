using Gestao.Core.DTO.Generic;
using Gestao.Core.DTO.Produto;
using Gestao.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestao.Core.Interfaces.Services
{
    public interface IProdutoServices : IGenericServices<ProdutoDTO, GGet<ObterProdutoDto>, GList<ObterProdutoDto>, MessageDTO>
    {
    }
}
