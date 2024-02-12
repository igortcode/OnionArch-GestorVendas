using Gestao.Core.Entidades;
using Gestao.Core.Interfaces.Repository;
using Gestao.Data.Context;

namespace Gestao.Data.Repository
{
    public class ProdutoRepository : GenericRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
    }
}
