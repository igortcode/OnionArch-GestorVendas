using Gestao.Core.Entidades;
using Gestao.Core.Interfaces.Repository;
using Gestao.Data.Context;
using Gestao.Data.Repository;

namespace MioDeBaoGestao.Repository
{
    public class ComandaRepository : GenericRepository<Comanda>, IComandaRepository
    {
        public ComandaRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
    }
}
