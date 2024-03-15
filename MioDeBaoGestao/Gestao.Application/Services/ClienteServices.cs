using AutoMapper;
using Gestao.Application.DTO.Cliente;
using Gestao.Application.DTO.Comanda;
using Gestao.Application.DTO.Generic;
using Gestao.Application.Enums;
using Gestao.Application.Interfaces.Repository;
using Gestao.Application.Interfaces.Services;
using Gestao.Core.Entidades;
using Gestao.Core.Validations.Exceptions;
using Gestao.Core.ValuableObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestao.Application.Services
{
    public class ClienteServices : IClienteServices
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteServices(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<MessageDTO> AdicionarAsync(ClienteDTO dto)
        {
            try
            {

                if (!string.IsNullOrEmpty(dto.Cpf))
                    if (await _clienteRepository.AnyAsync(a => a.CPF.Value.Equals(dto.Cpf)))
                        throw new DomainExceptionValidate("Já existe um cliente cadastrado com esse cpf!");

                var entity = _mapper.Map<Cliente>(dto);

                await _clienteRepository.AddAsync(entity);

                return new MessageDTO("Cadastro efetuado com sucesso!");
            }
            catch (DomainExceptionValidate dev)
            {
                return new MessageDTO(dev.Message, Enums.TipoNotificacao.Alert, dev);
            }
            catch (Exception ex)
            {
                return new MessageDTO("Erro ao cadastrar um cliente!", Enums.TipoNotificacao.Erro, ex);
            }
        }

        public async Task<MessageDTO> AtualizarAsync(int id, ClienteDTO dto)
        {
            try
            {
                if (await _clienteRepository.AnyAsync(a => a.CPF.Value.Equals(dto.Cpf) && a.Id != id))
                    throw new DomainExceptionValidate("Já existe um cliente cadastrado com esse cpf!");

                var entity = await _clienteRepository.FirstOrDefaultAsync(a => a.Id == id);
                if (entity == null)
                    throw new InvalidOperationException("Entidade não foi encontrada com o identificador informado.");

                entity.Atualizar(dto.Nome, new CpfVO(dto.Cpf));

                await _clienteRepository.UpdateAsync(entity);

                return new MessageDTO("Cadastro atualizado com sucesso!");
            }
            catch (DomainExceptionValidate dev)
            {
                return new MessageDTO(dev.Message, Enums.TipoNotificacao.Alert, dev);
            }
            catch (Exception ex)
            {
                return new MessageDTO("Erro ao atualizar o cliente!", Enums.TipoNotificacao.Erro, ex);
            }
        }

        public async Task<GGet<ObterClienteDTO>> BuscarPorIdAsync(int id)
        {

            try
            {
                var entity = await _clienteRepository.FirstOrDefaultAsync(a => a.Id == id);

                return new GGet<ObterClienteDTO>
                {
                    DTO = _mapper.Map<ObterClienteDTO>(entity),
                    Message = new MessageDTO("Busca efetuada com sucesso!")
                };

            }
            catch (Exception ex)
            {
                return new GGet<ObterClienteDTO> { Message = new MessageDTO("Erro ao buscar o cliente.", TipoNotificacao.Erro, ex) };
            }
        }

        public async Task<MessageDTO> ExcluirAsync(int id)
        {
            try
            {
                var entity = await _clienteRepository.FirstOrDefaultAsync(a => a.Id == id);
                if (entity is null) throw new InvalidOperationException("Entidade não encontrada para esse identificador.");

                await _clienteRepository.DeleteAsync(entity);

                return new MessageDTO("Exclusão efetuada com sucesso!");
            }
            catch (InvalidOperationException ioe)
            {
                return new MessageDTO(ioe.Message, TipoNotificacao.Erro, ioe);
            }
            catch (Exception ex)
            {
                return new MessageDTO("Erro ao excluir o cliente.", TipoNotificacao.Erro, ex);
            }
        }

        public async Task<GList<ObterClienteDTO>> ListarAsync()
        {
            try
            {
                var entity = await _clienteRepository.GetAllAsync();

                return new GList<ObterClienteDTO>
                {
                    DTOs = _mapper.Map<IList<ObterClienteDTO>>(entity),
                    Message = new MessageDTO("Busca efetuada com sucesso!")
                };

            }
            catch (Exception ex)
            {
                return new GList<ObterClienteDTO> { Message = new MessageDTO("Erro ao buscar o cliente.", TipoNotificacao.Erro, ex) };
            }
        }

        public async Task<GList<ObterClienteDTO>> ListarPaginadoAsync(int page, int pageSize)
        {
            try
            {
                var entity = await _clienteRepository.ListarPaginadoAsync(page, pageSize);

                return new GList<ObterClienteDTO>
                {
                    DTOs = entity.Item1,
                    MetaData = entity.Item2,
                    Message = new MessageDTO("Busca efetuada com sucesso!")
                };

            }
            catch (Exception ex)
            {
                return new GList<ObterClienteDTO> { Message = new MessageDTO("Erro ao buscar a comanda.", TipoNotificacao.Erro, ex) };
            }
        }

        public async Task<GList<ObterClienteDTO>> PesquisarPaginadoAsync(string search, int page, int pageSize)
        {
            try
            {
                var entity = await _clienteRepository.PesquisarPaginadoAsync(search, page, pageSize);

                return new GList<ObterClienteDTO>
                {
                    DTOs = entity.Item1,
                    MetaData = entity.Item2,
                    Message = new MessageDTO("Busca efetuada com sucesso!")
                };

            }
            catch (Exception ex)
            {
                return new GList<ObterClienteDTO> { Message = new MessageDTO("Erro ao buscar a comanda.", TipoNotificacao.Erro, ex) };
            }
        }
    }
}
