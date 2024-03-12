using Gestao.Core.Entidades;
using Gestao.Application.Interfaces.Repository;
using Gestao.Data.Context;
using System.Threading.Tasks;
using System.Collections.Generic;
using Gestao.Application.DTO.Produto;
using Gestao.Application.DTO.Generic;
using System.Linq;
using X.PagedList;
using Gestao.Data.Helper;

namespace Gestao.Data.Repository
{
    public class ProdutoRepository : GenericRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }

        public async Task<(IList<ObterProdutoDto>, PagedListMetaDataDTO)> ListarPaginadoAsync(int page, int pageSize)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 5 : pageSize;

            var result = await _context.Produtos.ToPagedListAsync(page, pageSize);

            result.GetMetaData().TryParceMetaDataDTO(out var metaDataDTO);

            var dtos = result.Select(a => new ObterProdutoDto
            {
                Id = a.Id,
                Preco = a.Preco,
                Nome = a.Nome,
                Quantidade = a.Quantidade,

            }).ToList();


            return new(dtos, metaDataDTO);
        }

        public async Task<(IList<ObterProdutoDto>, PagedListMetaDataDTO)> PesquisarPaginadoAsync(string search, int page, int pageSize)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 5 : pageSize;
            
            var query = _context.Produtos.AsQueryable();

            if(decimal.TryParse(search, out var value))
            {
                query = query.Where(a => a.Quantidade == (int)value || a.Preco == value || a.Nome.Contains(search));
            }
            else
            {
                query = query.Where(a => a.Nome.Contains(search));
            }

            var result = await query.ToPagedListAsync(page, pageSize);

            result.GetMetaData().TryParceMetaDataDTO(out var metaDataDTO);

            var dtos = result.Select(a => new ObterProdutoDto
            {
                Id = a.Id,
                Preco = a.Preco,
                Nome = a.Nome,
                Quantidade = a.Quantidade,

            }).ToList();


            return new(dtos, metaDataDTO);
        }
    }
}
