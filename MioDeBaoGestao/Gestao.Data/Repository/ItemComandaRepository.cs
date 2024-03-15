using Gestao.Core.Entidades;
using Gestao.Application.Interfaces.Repository;
using Gestao.Data.Context;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Gestao.Application.DTO.ItemComanda;
using Gestao.Application.DTO.Generic;
using X.PagedList;
using System.Linq;
using Gestao.Data.Helper;

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

        public async Task<(IList<ObterItemComandaDTO>, PagedListMetaDataDTO)> ListarPaginadoPorIdComandaAsync(int idComanda, int page, int pageSize)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 5 : pageSize;

            var result = await _context.ItensComanda.Include(a => a.Comanda).Include(a => a.Produto).Where(a => a.ComandaId == idComanda).ToPagedListAsync(page, pageSize);

            result.GetMetaData().TryParceMetaDataDTO(out var metaDataDTO);

            var dtos = result.Select(a => new ObterItemComandaDTO
            {
                Id = a.Id,
                Preco = a.Preco,
                Nome = a.Nome,
                Quantidade = a.Quantidade,
                ComandaId= a.ComandaId,
                NmComanda = a.Comanda.Nome,
                NmProdutoPai = a.Produto.Nome,
                ProdutoId = a.ProdutoId

            }).ToList();


            return new(dtos, metaDataDTO);
        }

        public async Task<(IList<ObterItemComandaDTO>, PagedListMetaDataDTO)> PesquisarPaginadoPorIdComandaAsync(string search, int idComanda, int page, int pageSize)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 5 : pageSize;

            var query = _context.ItensComanda.Include(a => a.Comanda).Include(a => a.Produto).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                if (decimal.TryParse(search, out var value))
                {
                    query = query.Where(a => a.ComandaId == idComanda && (a.Quantidade == (int)value || a.Preco == value || a.Nome.Contains(search)));
                }
                else
                {
                    query = query.Where(a => a.ComandaId == idComanda && a.Nome.Contains(search));
                }

            }
            else
            {
                query = query.Where(a => a.ComandaId == idComanda);
            }


            var result = await query.ToPagedListAsync(page, pageSize);

            result.GetMetaData().TryParceMetaDataDTO(out var metaDataDTO);

            var dtos = result.Select(a => new ObterItemComandaDTO
            {
                Id = a.Id,
                Preco = a.Preco,
                Nome = a.Nome,
                Quantidade = a.Quantidade,
                ComandaId = a.ComandaId,
                NmComanda = a.Comanda.Nome,
                NmProdutoPai = a.Produto.Nome,
                ProdutoId = a.ProdutoId

            }).ToList();


            return new(dtos, metaDataDTO);
        }
    }
}
