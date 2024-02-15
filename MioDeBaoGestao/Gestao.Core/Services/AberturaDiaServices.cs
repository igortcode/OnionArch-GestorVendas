﻿using AutoMapper;
using Gestao.Core.DTO.AberturaDia;
using Gestao.Core.DTO.Generic;
using Gestao.Core.Entidades;
using Gestao.Core.Interfaces.Repository;
using Gestao.Core.Interfaces.Services;
using Gestao.Core.Validations.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestao.Core.Services
{
    public class AberturaDiaServices : IAberturaDiaServices
    {
        private readonly IAberturaDiaRepository _aberturaDiaRepository;
        private readonly IMapper _mapper;

        public AberturaDiaServices(IAberturaDiaRepository aberturaDiaRepository, IMapper mapper)
        {
            _aberturaDiaRepository = aberturaDiaRepository;
            _mapper = mapper;
        }

        public async Task<MessageDTO> AbrirDiaAsync()
        {
            try
            {
                if (!await _aberturaDiaRepository.AnyAsync(a => a.DataAbertura == DateTime.Now.Date && a.Aberta))
                    throw new DomainExceptionValidate("Já existe um dia aberto.");

                var dia = await _aberturaDiaRepository.FirstOrDefaultAsync(a => a.DataAbertura == DateTime.Now);

                if (dia is null)
                {
                    dia = new AberturaDia();

                    await _aberturaDiaRepository.AddAsync(dia);

                    return new MessageDTO("Dia aberto com sucesso.");
                }

                dia.AbrirDia();

                await _aberturaDiaRepository.UpdateAsync(dia);

                return new MessageDTO("Dia reaberto com sucesso.");

            }
            catch (DomainExceptionValidate dev)
            {
                return new MessageDTO(dev.Message, Enums.TipoNotificacao.Alert, dev);
            }
            catch (Exception ex)
            {
                return new MessageDTO("Erro ao efetuar a abertura do dia", Enums.TipoNotificacao.Alert, ex);
            }
        }

        public async Task<GGet<AberturaDiaDTO>> BuscarAberturaDiaPorId(int id)
        {
            try
            {
                var aberturaDia = await _aberturaDiaRepository.FirstOrDefaultAsync(a => a.Id == id);

                return new GGet<AberturaDiaDTO>
                {
                    DTO = _mapper.Map<AberturaDiaDTO>(aberturaDia),
                    Message = new MessageDTO("Busca efetuada com sucesso")
                };

            }
            catch (Exception ex)
            {
                return new GGet<AberturaDiaDTO> { Message = new MessageDTO("Erro ao buscar a abertura do dia", Enums.TipoNotificacao.Alert, ex) };
            }
        }

        public async Task<MessageDTO> FecharDiaAsync()
        {
            try
            {
                if (!await _aberturaDiaRepository.AnyAsync(a => a.DataAbertura == DateTime.Now.Date && !a.Aberta))
                    throw new DomainExceptionValidate("O dia já esta fechado.");

                var dia = await _aberturaDiaRepository.FirstOrDefaultAsync(a => a.DataAbertura == DateTime.Now);

                if (dia is null)
                    throw new InvalidOperationException("Não foi encontrado nenhum dia cadastrado.");

                dia.FecharDia();

                await _aberturaDiaRepository.UpdateAsync(dia);

                return new MessageDTO("Dia reaberto com sucesso.");

            }
            catch (DomainExceptionValidate dev)
            {
                return new MessageDTO(dev.Message, Enums.TipoNotificacao.Alert, dev);
            }
            catch (Exception ex)
            {
                return new MessageDTO("Erro ao efetuar a abertura do dia", Enums.TipoNotificacao.Alert, ex);
            }
        }

        public async Task<GList<AberturaDiaDTO>> ListarAberturasDiasAsync()
        {

            try
            {
                var aberturaDia = await _aberturaDiaRepository.GetAllAsync();

                return new GList<AberturaDiaDTO>
                {
                    DTOs = _mapper.Map<IList<AberturaDiaDTO>>(aberturaDia),
                    Message = new MessageDTO("Busca efetuada com sucesso")
                };

            }
            catch (Exception ex)
            {
                return new GList<AberturaDiaDTO> { Message = new MessageDTO("Erro ao buscar a abertura do dia", Enums.TipoNotificacao.Alert, ex) };
            }
        }
    }
}