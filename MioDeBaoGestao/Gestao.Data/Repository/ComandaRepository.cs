using Gestao.Core.Entidades;
using Gestao.Application.Interfaces.Repository;
using Gestao.Data.Context;
using Gestao.Data.Repository;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace MioDeBaoGestao.Repository
{
    public class ComandaRepository : GenericRepository<Comanda>, IComandaRepository
    {
        public ComandaRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }

        public override async Task<Comanda> FirstOrDefaultAsync(Expression<Func<Comanda, bool>> expression)
        {
            return await _context.Comandas.Include(a => a.Itens).FirstOrDefaultAsync(expression);
        }

        public override async Task<IList<Comanda>> GetAllAsync()
        {
            return await _context.Comandas.Include(a => a.Itens).ToListAsync();
        }
    }
}
