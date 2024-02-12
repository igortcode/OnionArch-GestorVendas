using Gestao.Core.Entidades;
using Gestao.Core.Interfaces.Repository;
using Gestao.Data.Context;

namespace Gestao.Data.Repository
{
    public class AberturaDiaRepository : GenericRepository<AberturaDia>, IAberturaDiaRepository
    {
        public AberturaDiaRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
    }
}
