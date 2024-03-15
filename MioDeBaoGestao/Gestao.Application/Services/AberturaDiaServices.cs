using AutoMapper;
using Gestao.Application.DTO.AberturaDia;
using Gestao.Application.DTO.Generic;
using Gestao.Application.DTO.RelatorioDTO;
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
                if(await _aberturaDiaRepository.AnyAsync(a => a.Status))
                    return new MessageDTO("Existe um dia aberto. Para abrir outro dia é necessário fechar o anterior.", Enums.TipoNotificacao.Alert);

                if (await _aberturaDiaRepository.AnyAsync(a => a.DataAbertura.Date == DateTime.Now.Date && a.Status))
                    return new MessageDTO("Já existe um dia aberto.", Enums.TipoNotificacao.Alert);

                var dia = await _aberturaDiaRepository.FirstOrDefaultAsync(a => a.DataAbertura.Date == DateTime.Now.Date);

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

        public async Task<MessageDTO> FecharDiaAsync(int id)
        {
            try
            {
                if (await _aberturaDiaRepository.AnyAsync(a => a.DataAbertura.Date == DateTime.Now.Date && !a.Status))
                    throw new DomainExceptionValidate("O dia já esta fechado.");

                var dia = await _aberturaDiaRepository.FirstOrDefaultAsync(a => a.Id == id && a.Status);

                if (dia is null)
                    throw new InvalidOperationException("Não foi encontrado nenhum dia cadastrado.");

                dia.FecharDia();

                await _aberturaDiaRepository.UpdateAsync(dia);

                return new MessageDTO("Dia fechado com sucesso.");

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

        public async Task<GList<RelatorioVendasDTO>> RelatorioVendasAsync()
        {

            try
            {
                var aberturaDia = await _aberturaDiaRepository.GetByExpressio(a => !a.Status);

                var relatorioPorMes = aberturaDia.GroupBy(a => a.DataAbertura.Month);

                var result = new List<RelatorioVendasDTO>();

                foreach(var itens in relatorioPorMes)
                {
                    result.Add(new RelatorioVendasDTO
                    {
                        Mes = BuscarMes(itens.FirstOrDefault().DataAbertura.Month),
                        DiasTrabalhados = itens.Count(),
                        ValorTotal = itens.Sum(a => a.Faturamento ?? 0),
                        Ano = itens.FirstOrDefault().DataAbertura.Year
                    });
                }

                return new GList<RelatorioVendasDTO>
                {
                    DTOs = result,
                    Message = new MessageDTO("Busca efetuada com sucesso")
                };

            }
            catch (Exception ex)
            {
                return new GList<RelatorioVendasDTO> { Message = new MessageDTO("Erro ao buscar o relatório do dia", Enums.TipoNotificacao.Alert, ex) };
            }
        }

        private string BuscarMes(int id)
        {
            switch (id)
            {
                case 1:
                    return "Janeiro";
                case 2:
                    return "Fevereiro";
                case 3:
                    return "Março";
                case 4:
                    return "Abril";
                case 5:
                    return "Maio";
                case 6:
                    return "Junho";
                case 7:
                    return "Julho";
                case 8:
                    return "Agosto";
                case 9:
                    return "Setembro";
                case 10:
                    return "Outubro";
                case 11:
                    return "Novembro";
                case 12:
                    return "Dezembro";
                default:
                    return string.Empty;


            }
        }
    }
}
