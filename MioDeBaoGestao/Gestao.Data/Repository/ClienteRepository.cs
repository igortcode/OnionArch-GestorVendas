using Gestao.Core.Entidades;
using Gestao.Application.Interfaces.Repository;
using Gestao.Data.Context;
using Gestao.Data.Repository;
using System.Threading.Tasks;
using System.Collections.Generic;
using Gestao.Application.DTO.Cliente;
using Gestao.Application.DTO.Generic;
using X.PagedList;
using System.Linq;
using Gestao.Data.Helper;

namespace MioDeBaoGestao.Repository
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }

        public async Task<(IList<ObterClienteDTO>, PagedListMetaDataDTO)> ListarPaginadoAsync(int page, int pageSize)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 5 : pageSize;

            var result = await _context.Clientes.Select(a => new ObterClienteDTO
            {
                Id = a.Id,
                Cpf = a.CPF.Value,
                Nome = a.Nome
            }).ToPagedListAsync(page, pageSize);

            result.GetMetaData().TryParceMetaDataDTO(out var metaDataDTO);


            return new(result.ToList(), metaDataDTO);
        }

        public async Task<(IList<ObterClienteDTO>, PagedListMetaDataDTO)> PesquisarPaginadoAsync(string search, int page, int pageSize)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 5 : pageSize;

            var result = await _context.Clientes
                .Where(a => a.Nome.Contains(search) || a.CPF.Value.Contains(search)).Select(a => new ObterClienteDTO
                {
                    Id = a.Id,
                    Cpf = a.CPF.Value,
                    Nome = a.Nome
                }).ToPagedListAsync(page, pageSize);

            result.GetMetaData().TryParceMetaDataDTO(out var metaDataDTO);


            return new(result.ToList(), metaDataDTO);
        }
    }
}
