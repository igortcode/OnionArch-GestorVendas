using AutoMapper;
using Gestao.Application.DTO.Comanda;
using Gestao.Application.DTO.Generic;
using Gestao.Application.Enums;
using Gestao.Application.Interfaces.Repository;
using Gestao.Application.Interfaces.Services;
using Gestao.Core.Entidades;
using Gestao.Core.Validations.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestao.Application.Services
{
    public class ComandaServices : IComandaServices
    {
        private readonly IComandaRepository _comandaRepository;
        private readonly IMapper _mapper;

        public ComandaServices(IComandaRepository comandaRepository, IMapper mapper)
        {
            _comandaRepository = comandaRepository;
            _mapper = mapper;
        }

        public async Task<MessageDTO> AdicionarAsync(ComandaDTO dto)
        {
            try
            {
                if (await _comandaRepository.AnyAsync(a => a.Nome.ToLower().Equals(dto.Nome.ToLower()) && a.AberturaDiaId == dto.AberturaDiaId))
                    throw new DomainExceptionValidate("Já existe uma comanda com esse nome.");

                var entity = _mapper.Map<Comanda>(dto);

                await _comandaRepository.AddAsync(entity);

                return new MessageDTO("Cadastro efetuado com sucesso!");
            }
            catch (DomainExceptionValidate dev)
            {
                return new MessageDTO(dev.Message, Enums.TipoNotificacao.Alert, dev);
            }
            catch (Exception ex)
            {
                return new MessageDTO("Erro ao cadastrar uma comanda!", Enums.TipoNotificacao.Erro, ex);
            }
        }

        public async Task<MessageDTO> AtualizarAsync(int id, ComandaDTO dto)
        {
            try
            {
                if (await _comandaRepository.AnyAsync(a => a.Nome.ToLower().Equals(dto.Nome.ToLower()) && a.AberturaDiaId == dto.AberturaDiaId && a.Id != id))
                    throw new DomainExceptionValidate("Já existe uma comanda com esse nome.");

                var entity = await _comandaRepository.FirstOrDefaultAsync(a => a.Id == id);
                if (entity == null)
                    throw new ArgumentException("Não foi encontrado nenhuma entidade com esse identificador.");

                entity.Atualizar(dto.Nome, dto.ClienteId);

                await _comandaRepository.UpdateAsync(entity);

                return new MessageDTO("Cadastro atualizado com sucesso!");
            }
            catch (DomainExceptionValidate dev)
            {
                return new MessageDTO(dev.Message, Enums.TipoNotificacao.Alert, dev);
            }
            catch (Exception ex)
            {
                return new MessageDTO("Erro ao atualizar uma comanda!", Enums.TipoNotificacao.Erro, ex);
            }
        }

        public async Task<GGet<ObterComandaDTO>> BuscarPorIdAsync(int id)
        {

            try
            {
                var entity = await _comandaRepository.FirstOrDefaultAsync(a => a.Id == id);

                return new GGet<ObterComandaDTO>
                {
                    DTO = _mapper.Map<ObterComandaDTO>(entity),
                    Message = new MessageDTO("Busca efetuada com sucesso!")
                };

            }
            catch (Exception ex)
            {
                return new GGet<ObterComandaDTO> { Message = new MessageDTO("Erro ao buscar a comanda.", TipoNotificacao.Erro, ex) };
            }
        }

        public async Task<GGet<ObterComandaDTO>> BuscarPorIdEAberturadiaAsync(int id, int idAberturaDia)
        {
            try
            {
                var entity = await _comandaRepository.FirstOrDefaultAsync(a => a.Id == id && a.AberturaDiaId == idAberturaDia);

                return new GGet<ObterComandaDTO>
                {
                    DTO = _mapper.Map<ObterComandaDTO>(entity),
                    Message = new MessageDTO("Busca efetuada com sucesso!")
                };

            }
            catch (Exception ex)
            {
                return new GGet<ObterComandaDTO> { Message = new MessageDTO("Erro ao buscar a comanda.", TipoNotificacao.Erro, ex) };
            }
        }

        public GGet<int> BuscarUltimoIdRegistrado()
        {

            try
            {
                var entity = _comandaRepository.GetQueryable().OrderByDescending(a => a.Id).FirstOrDefault();

                return new GGet<int>
                {
                    DTO = entity == null ? 0 : entity.Id,
                    Message = new MessageDTO("Busca efetuada com sucesso!")
                };

            }
            catch (Exception ex)
            {
                return new GGet<int> { Message = new MessageDTO("Erro ao buscar a comanda.", TipoNotificacao.Erro, ex) };
            }
        }

        public async Task<MessageDTO> ExcluirAsync(int id, int idAbertura)
        {
            try
            {
                var entity = await _comandaRepository.FirstOrDefaultAsync(a => a.Id == id && a.AberturaDiaId == idAbertura);
                if (entity is null) throw new ArgumentException("Entidade não encontrada para esse identificador.");

                await _comandaRepository.DeleteAsync(entity);

                return new MessageDTO("Exclusão efetuada com sucesso!");
            }
            catch (InvalidOperationException ioe)
            {
                return new MessageDTO(ioe.Message, TipoNotificacao.Erro, ioe);
            }
            catch (Exception ex)
            {

                return new MessageDTO("Erro ao excluir a comanda.", TipoNotificacao.Erro, ex);
            }
        }

        public async Task<MessageDTO> FecharComanda(int id, int idAberturaDia)
        {
            try
            {
                var entity = await _comandaRepository.FirstOrDefaultAsync(a => a.Id == id && a.AberturaDiaId == a.AberturaDiaId);
                if (entity is null) throw new ArgumentException("Entidade não encontrada para esse identificador.");

                entity.FecharComanda();

                entity.CalculaValorTotal();

                await _comandaRepository.UpdateAsync(entity);

                return new MessageDTO("Comanda fechada com sucesso!");
            }
            catch (InvalidOperationException ioe)
            {
                return new MessageDTO(ioe.Message, TipoNotificacao.Erro, ioe);
            }
            catch (Exception ex)
            {

                return new MessageDTO("Erro ao fechar a comanda.", TipoNotificacao.Erro, ex);
            }
        }

        public async Task<GList<ObterComandaDTO>> ListarPorAberturaDiaAsync(int idAberturaDia)
        {
            try
            {
                var entity = await _comandaRepository.GetByExpressio(a => a.AberturaDiaId == idAberturaDia);

                return new GList<ObterComandaDTO>
                {
                    DTOs = _mapper.Map<IList<ObterComandaDTO>>(entity),
                    Message = new MessageDTO("Busca efetuada com sucesso!")
                };

            }
            catch (Exception ex)
            {
                return new GList<ObterComandaDTO> { Message = new MessageDTO("Erro ao buscar a comanda.", TipoNotificacao.Erro, ex) };
            }
        }
    }
}
