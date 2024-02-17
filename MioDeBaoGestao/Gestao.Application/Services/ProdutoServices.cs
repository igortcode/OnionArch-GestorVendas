using AutoMapper;
using Gestao.Application.DTO.Generic;
using Gestao.Application.DTO.Produto;
using Gestao.Core.Entidades;
using Gestao.Application.Enums;
using Gestao.Application.Interfaces.Repository;
using Gestao.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestao.Application.Services
{
    public class ProdutoServices : IProdutoServices
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoServices(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<MessageDTO> AdicionarAsync(ProdutoDTO dto)
        {
            try
            {

                var entity = _mapper.Map<Produto>(dto);

                await _produtoRepository.AddAsync(entity);

                return new MessageDTO("Cadastro efetuado com sucesso!");
            }
            catch (Exception ex)
            {

                return new MessageDTO("Erro ao cadastrar o produto.", TipoNotificacao.Erro, ex);
            }
        }

        public async Task<MessageDTO> AtualizarAsync(int id, ProdutoDTO dto)
        {
            try
            {
                var entity = await _produtoRepository.FirstOrDefaultAsync(a => a.Id == id);
                if (entity is null) throw new InvalidOperationException("Entidade não encontrada para esse identificador.");

                entity.Atualizar(dto.Nome, dto.Preco, dto.Quantidade);

                await _produtoRepository.UpdateAsync(entity);

                return new MessageDTO("Cadastro atualizado com sucesso!");
            }
            catch (InvalidOperationException ioe)
            {
                return new MessageDTO(ioe.Message, TipoNotificacao.Erro, ioe);
            }
            catch (Exception ex)
            {

                return new MessageDTO("Erro ao atualizar o produto.", TipoNotificacao.Erro, ex);
            }
        }

        public async Task<GGet<ObterProdutoDto>> BuscarPorIdAsync(int id)
        {
            try
            {
                var entity = await _produtoRepository.FirstOrDefaultAsync(a => a.Id == id);

                return new GGet<ObterProdutoDto>
                {
                    DTO = _mapper.Map<ObterProdutoDto>(entity),
                    Message = new MessageDTO("Busca efetuada com sucesso!")
                };

            }
            catch (Exception ex)
            {

                return new GGet<ObterProdutoDto> { Message = new MessageDTO("Erro ao buscar o produto.", TipoNotificacao.Erro, ex) };
            }
        }

        public async Task<MessageDTO> ExcluirAsync(int id)
        {
            try
            {
                var entity = await _produtoRepository.FirstOrDefaultAsync(a => a.Id == id);
                if (entity is null) throw new InvalidOperationException("Entidade não encontrada para esse identificador.");

                await _produtoRepository.DeleteAsync(entity);

                return new MessageDTO("Exclusão efetuada com sucesso!");
            }
            catch (InvalidOperationException ioe)
            {
                return new MessageDTO(ioe.Message, TipoNotificacao.Erro, ioe);
            }
            catch (Exception ex)
            {

                return new MessageDTO("Erro ao excluir o produto.", TipoNotificacao.Erro, ex);
            }
        }

        public async Task<GList<ObterProdutoDto>> ListarAsync()
        {
            try
            {
                var entities = await _produtoRepository.GetAllAsync();

                return new GList<ObterProdutoDto>
                {
                    DTOs = _mapper.Map<List<ObterProdutoDto>>(entities),
                    Message = new MessageDTO("Busca efetuada com sucesso!")
                };

            }
            catch (Exception ex)
            {
                return new GList<ObterProdutoDto> { Message = new MessageDTO("Erro ao buscar o produto.", TipoNotificacao.Erro, ex) };
            }
        }       
    }
}
