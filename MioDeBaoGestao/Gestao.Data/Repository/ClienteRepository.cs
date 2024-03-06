using Gestao.Core.Entidades;
using Gestao.Application.Interfaces.Repository;
using Gestao.Data.Context;
using Gestao.Data.Repository;

namespace MioDeBaoGestao.Repository
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
    }
}
