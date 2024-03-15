using AutoMapper;
using Gestao.Application.Enums;
using Gestao.Application.Interfaces.Services;
using Gestao.Core.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MioDeBaoGestao.Models.AberturaDia;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MioDeBaoGestao.Controllers
{
    [Authorize]
    public class AberturaDiaController : BasicController
    {
        private readonly IAberturaDiaServices _aberturaDiaServices;
        private readonly IMapper _mapper;

        public AberturaDiaController(IAberturaDiaServices aberturaDiaServices, IMapper mapper)
        {
            _aberturaDiaServices = aberturaDiaServices;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string message, TipoNotificacao? tipoNotificacao)
        {
            var dtos = await _aberturaDiaServices.ListarAberturasDiasAsync();

            var viewModels = _mapper.Map<IList<AberturaDiaViewModel>>(dtos.DTOs);

            var diaVigente = dtos.DTOs.FirstOrDefault(a => a.Status == true);

            if (diaVigente != null && User.IsInRole("Vendedor"))
            {
                return RedirectToAction("Index", "Comanda", new { idAberturaDia  = diaVigente.Id});
            }

            if (!string.IsNullOrWhiteSpace(message))
            {
                AddNotification(tipoNotificacao.Value, message);
            }
            else
            {
                AddOnlyErrorsNotification(dtos.Message);
            }

            return AdapterView(viewModels, dtos.Message);
        }

        [Authorize(Roles = "Admin, Gerente")]
        public async Task<IActionResult> AbrirDia()
        {
            var result = await _aberturaDiaServices.AbrirDiaAsync();

            AddNotification(result);

            var dtos = await _aberturaDiaServices.ListarAberturasDiasAsync();

            var viewModels = _mapper.Map<IList<AberturaDiaViewModel>>(dtos.DTOs);

            return AdapterView("Index", viewModels, dtos.Message);
        }

        [Authorize(Roles = "Admin, Gerente")]
        public async Task<IActionResult> FecharDia(int id)
        {
            var result = await _aberturaDiaServices.FecharDiaAsync(id);

            AddNotification(result);

            var dtos = await _aberturaDiaServices.ListarAberturasDiasAsync();

            var viewModels = _mapper.Map<IList<AberturaDiaViewModel>>(dtos.DTOs);

            return AdapterView("Index", viewModels, dtos.Message);
        }
    }
}
