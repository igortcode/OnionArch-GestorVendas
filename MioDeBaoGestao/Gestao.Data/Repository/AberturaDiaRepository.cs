using Gestao.Application.Interfaces.Repository;
using Gestao.Core.Entidades;
using Gestao.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Gestao.Data.Repository
{
    public class AberturaDiaRepository : GenericRepository<AberturaDia>, IAberturaDiaRepository
    {
        public AberturaDiaRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }

        public override Task<AberturaDia> FirstOrDefaultAsync(Expression<Func<AberturaDia, bool>> expression)
        {
            return _context.AberturaDias.Include(a => a.Comandas).ThenInclude(a => a.Itens).FirstOrDefaultAsync(expression);
        }
    }
}
