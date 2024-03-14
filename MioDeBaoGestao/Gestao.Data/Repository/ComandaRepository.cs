using Gestao.Core.Entidades;
using Gestao.Application.Interfaces.Repository;
using Gestao.Data.Context;
using Gestao.Data.Repository;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Gestao.Application.DTO.Generic;
using Gestao.Application.DTO.Comanda;
using X.PagedList;
using Gestao.Data.Helper;

namespace MioDeBaoGestao.Repository
{
    public class ComandaRepository : GenericRepository<Comanda>, IComandaRepository
    {
        public ComandaRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }

        public override async Task<Comanda> FirstOrDefaultAsync(Expression<Func<Comanda, bool>> expression)
        {
            return await _context.Comandas.Include(a => a.Itens).Include(a => a.Cliente).FirstOrDefaultAsync(expression);
        }

        public override async Task<IList<Comanda>> GetAllAsync()
        {
            return await _context.Comandas.Include(a => a.Itens).ToListAsync();
        }

        public override async Task<IEnumerable<Comanda>> GetByExpressio(Expression<Func<Comanda, bool>> expression)
        {
            return await _context.Comandas.Include(a => a.Itens).Include(a => a.Cliente).Where(expression).ToListAsync();
        }

        public async Task<(IList<ObterComandaDTO>, PagedListMetaDataDTO)> ListarPaginadoAsync(int aberturaDiaId, int page, int pageSize)
        {
            var result = await _context.Comandas.Include(a => a.Itens).Include(a => a.AberturaDia).Include(a => a.Cliente).Where(a => a.AberturaDiaId == aberturaDiaId).OrderByDescending(a => a.DtCriacao).ToPagedListAsync(page, pageSize);
          
            result.GetMetaData().TryParceMetaDataDTO(out var metaData);

            return (castToDto(result.ToList()), metaData);
        }

        

        public async Task<(IList<ObterComandaDTO>, PagedListMetaDataDTO)> PesquisarPaginadoAsync(int aberturaDiaId, string search, int page, int pageSize)
        {
            var query =  _context.Comandas.Include(a => a.Itens).Include(a => a.AberturaDia).Include(a => a.Cliente).Where(a => a.AberturaDiaId == aberturaDiaId);

            if (!string.IsNullOrWhiteSpace(search))
            {

                if (decimal.TryParse(search, out var valor))
                {
                    query = query.Where(a => a.Total == valor || a.Nome.Contains(search));
                }
                else
                {
                    query = query.Where(a => a.Nome.Contains(search));
                }
            }

            var result = await query.OrderByDescending(a => a.DtCriacao).ToPagedListAsync(page, pageSize);

            result.GetMetaData().TryParceMetaDataDTO(out var metaData);

            return (castToDto(result.ToList()), metaData);
        }

        private IList<ObterComandaDTO> castToDto(List<Comanda> comandas)
        {
            var result = comandas.Select(a => new ObterComandaDTO
            {
                AberturaDiaId = a.AberturaDiaId,
                ClienteId = a.ClienteId,
                ComandaFechada = a.ComandaFechada,
                DtCriacao = a.DtCriacao,
                Id = a.Id,
                NmCliente = a.Cliente?.Nome,
                Nome = a.Nome,
                Total = a.Total,
                DiaFechado = a.AberturaDia.Status               
            }).ToList();


            return result;
        }
    }
}
