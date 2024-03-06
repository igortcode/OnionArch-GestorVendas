using Gestao.Core.Entidades;
using Gestao.Application.Interfaces.Repository;
using Gestao.Data.Context;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Data.Repository
{
    public class ItemComandaRepository : GenericRepository<ItemComanda>, IItemComandaRepository
    {
        public ItemComandaRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }

        public override async Task<ItemComanda> FirstOrDefaultAsync(Expression<Func<ItemComanda, bool>> expression)
        {
           return await _context.ItensComanda.Include(a => a.Comanda).Include(a => a.Produto).FirstOrDefaultAsync(expression);
        }
    }
}
