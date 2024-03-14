using AutoMapper;
using Gestao.Application.DTO.Generic;
using Gestao.Application.DTO.ItemComanda;
using Gestao.Application.DTO.Produto;
using Gestao.Application.Enums;
using Gestao.Application.Interfaces.Repository;
using Gestao.Application.Interfaces.Services;
using Gestao.Core.Entidades;
using Gestao.Core.Validations.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Gestao.Application.Services
{
    public class ItemComandaServices : IItemComandaServices
    {
        private readonly IItemComandaRepository _itemComandaRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ItemComandaServices(IItemComandaRepository itemComandaRepository, IProdutoRepository produtoRepository, IMapper mapper)
        {
            _itemComandaRepository = itemComandaRepository;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<MessageDTO> AdicionarAsync(ItemComandaDTO dto)
        {
            try
            {
                var entity = await _itemComandaRepository.FirstOrDefaultAsync(a => a.ProdutoId == dto.ProdutoId && a.ComandaId == dto.ComandaId);

                var produto = await _produtoRepository.GetByIdAsync(dto.ProdutoId);
                if (produto is null)
                    throw new ArgumentException("Identificador inválido para o produto.");

                if(entity == null) 
                {
                    return await Cadastrar(dto, produto, entity);
                }
                else
                {
                    return await Atualizar(dto, produto, entity);
                }

            }
            catch (ArgumentException ae)
            {
                return new MessageDTO(ae.Message, Enums.TipoNotificacao.Erro, ae);
            }
            catch (DomainExceptionValidate dev)
            {
                return new MessageDTO(dev.Message, Enums.TipoNotificacao.Alert, dev);
            }
            catch (Exception ex)
            {
                return new MessageDTO("Erro ao cadastrar um item de comanda!", Enums.TipoNotificacao.Erro, ex);
            }
        }


        private async Task<MessageDTO> Cadastrar(ItemComandaDTO dto, Produto produto, ItemComanda itemComanda)
        {
            produto.RemoverQuantidade(dto.Quantidade);
            itemComanda = new ItemComanda(produto.Nome, produto.Preco, dto.Quantidade, dto.ProdutoId, dto.ComandaId);

            using(var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _produtoRepository.UpdateAsync(produto);
                await _itemComandaRepository.AddAsync(itemComanda);            

                transaction.Complete();
            }

            return new MessageDTO("Cadastro efetuado com sucesso!");
        }

        private async Task<MessageDTO> Atualizar(ItemComandaDTO dto, Produto produto, ItemComanda itemComanda)
        {
            produto.RemoverQuantidade(dto.Quantidade);
            itemComanda.AddQuantidade(dto.Quantidade);

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _produtoRepository.UpdateAsync(produto);
                await _itemComandaRepository.UpdateAsync(itemComanda);

                transaction.Complete();
            }

            return new MessageDTO("Cadastro atualizado com sucesso!");
        }

        public async Task<MessageDTO> AtualizarAsync(int id, ItemComandaDTO dto)
        {
            try
            {
                var entity = await _itemComandaRepository.FirstOrDefaultAsync(a => a.ProdutoId == dto.ProdutoId && a.ComandaId == dto.ComandaId && a.Id == id);
                if (entity == null)
                    throw new ArgumentException("Entidade não encontrada com esse identificador.");

                var produto = await _produtoRepository.GetByIdAsync(dto.ProdutoId);
                if (produto is null)
                    throw new ArgumentException("Identificador inválido para o produto.");


                return await Atualizar(dto, produto, entity);

            }
            catch (ArgumentException ae)
            {
                return new MessageDTO(ae.Message, Enums.TipoNotificacao.Erro, ae);
            }
            catch (DomainExceptionValidate dev)
            {
                return new MessageDTO(dev.Message, Enums.TipoNotificacao.Alert, dev);
            }
            catch (Exception ex)
            {
                return new MessageDTO("Erro ao atualizar um item de comanda!", Enums.TipoNotificacao.Erro, ex);
            }
        }

        public async Task<GGet<ObterItemComandaDTO>> ObterItemComandaPorIdEIdComanda(int id, int idComanda)
        {
            try
            {
                var entity = await _itemComandaRepository.FirstOrDefaultAsync(a => a.Id == id && a.ComandaId == idComanda);

                return new GGet<ObterItemComandaDTO>
                {
                    DTO = _mapper.Map<ObterItemComandaDTO>(entity),
                    Message = new MessageDTO("Busca efetuada com sucesso!")
                };

            }
            catch (Exception ex)
            {
                return new GGet<ObterItemComandaDTO> { Message = new MessageDTO("Erro ao buscar o item de comanda.", TipoNotificacao.Erro, ex) };
            }
        }

        public async Task<GList<ObterItemComandaDTO>> ListarItemComandaPorIdEIdComanda(int idComanda)
        {
            try
            {
                var entity = await _itemComandaRepository.GetByExpressio(a => a.ComandaId == idComanda);

                return new GList<ObterItemComandaDTO>
                {
                    DTOs = _mapper.Map<IList<ObterItemComandaDTO>>(entity),
                    Message = new MessageDTO("Busca efetuada com sucesso!")
                };

            }
            catch (Exception ex)
            {
                return new GList<ObterItemComandaDTO> { Message = new MessageDTO("Erro ao buscar o item de comanda.", TipoNotificacao.Erro, ex) };
            }
        }

        public async Task<MessageDTO> ExcluirItemComandaPorIdEIdComanda(int id, int idComanda, int quantidade)
        {
            try
            {
                var entity = await _itemComandaRepository.FirstOrDefaultAsync(a => a.ComandaId == idComanda && a.Id == id);
                if (entity is null) throw new ArgumentException("Entidade não encontrada com esse identificador!");

                if (entity.Comanda.ComandaFechada)
                    throw new DomainExceptionValidate("Essa comanda já esta fechada!");

                entity.RemoveQuantidade(quantidade);
                
                var produto = entity.Produto;
                produto.AdicionarQuantidade(quantidade);


                using(var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    await _produtoRepository.UpdateAsync(produto);

                    if(entity.Quantidade == 0)
                    {
                        await _itemComandaRepository.DeleteAsync(entity);
                    }
                    else
                    {
                        await _itemComandaRepository.UpdateAsync(entity);
                    }

                    transaction.Complete();
                }


                return new MessageDTO("Excluído com sucesso!");

            }
            catch(ArgumentException ae)
            {
                return new MessageDTO(ae.Message, TipoNotificacao.Erro, ae);
            }
            catch (Exception ex)
            {
                return new MessageDTO("Erro ao excluir o item de comanda.", TipoNotificacao.Erro, ex);
            }
        }

        public async Task<GList<ObterItemComandaDTO>> ListarItemComandaPorIdEIdComandaPaginadoAsync(int idComanda, int page, int pageSize)
        {
            try
            {
                var entity = await _itemComandaRepository.ListarPaginadoPorIdComandaAsync(idComanda, page, pageSize);

                return new GList<ObterItemComandaDTO>
                {
                    DTOs = entity.Item1,
                    MetaData = entity.Item2,
                    Message = new MessageDTO("Busca efetuada com sucesso!")
                };

            }
            catch (Exception ex)
            {
                return new GList<ObterItemComandaDTO> { Message = new MessageDTO("Erro ao buscar a comanda.", TipoNotificacao.Erro, ex) };
            }
        }

        public async Task<GList<ObterItemComandaDTO>> PesquisarItemComandaPorIdEIdComandaPaginadoAsync(string search, int idComanda, int page, int pageSize)
        {
            try
            {
                var entity = await _itemComandaRepository.PesquisarPaginadoPorIdComandaAsync(search, idComanda, page, pageSize);

                return new GList<ObterItemComandaDTO>
                {
                    DTOs = entity.Item1,
                    MetaData = entity.Item2,
                    Message = new MessageDTO("Busca efetuada com sucesso!")
                };

            }
            catch (Exception ex)
            {
                return new GList<ObterItemComandaDTO> { Message = new MessageDTO("Erro ao buscar a comanda.", TipoNotificacao.Erro, ex) };
            }
        }

        public async Task<GGet<decimal>> ObterSomatorioValorItemComandaAsync(int idComanda)
        {
            try
            {
                if(!await _itemComandaRepository.AnyAsync(a => a.ComandaId == idComanda))
                    return new GGet<decimal> { DTO = 0, Message = new MessageDTO("Busca efetuada com sucesso")};


                var value = _itemComandaRepository.GetQueryable().Where(a => a.ComandaId == idComanda).Sum(a => (a.Quantidade * a.Preco));              
                return new GGet<decimal> { DTO = value, Message = new MessageDTO("Busca efetuada com sucesso!")};
            }
            catch (Exception ex)
            {
                return new GGet<decimal> { Message = new MessageDTO("Erro ao buscar o valor da comanda.", TipoNotificacao.Erro, ex) };
            }
        }
    }
}
