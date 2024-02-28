using AutoMapper;
using Gestao.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using MioDeBaoGestao.Models.AberturaDia;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MioDeBaoGestao.Controllers
{
    public class AberturaDiaController : BasicController
    {
        private readonly IAberturaDiaServices _aberturaDiaServices;
        private readonly IMapper _mapper;

        public AberturaDiaController(IAberturaDiaServices aberturaDiaServices, IMapper mapper)
        {
            _aberturaDiaServices = aberturaDiaServices;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var dtos = await _aberturaDiaServices.ListarAberturasDiasAsync();

            var viewModels = _mapper.Map<IList<AberturaDiaViewModel>>(dtos.DTOs);

            AddOnlyErrorsNotification(dtos.Message);

            return AdapterView(viewModels, dtos.Message);
        }

        public async Task<IActionResult> AbrirDia()
        {
            var result = await _aberturaDiaServices.AbrirDiaAsync();

            AddNotification(result);

            var dtos = await _aberturaDiaServices.ListarAberturasDiasAsync();

            var viewModels = _mapper.Map<IList<AberturaDiaViewModel>>(dtos.DTOs);

            return AdapterView("Index", viewModels, dtos.Message);
        }

        public async Task<IActionResult> FecharDia()
        {
            var result = await _aberturaDiaServices.FecharDiaAsync();

            AddNotification(result);

            var dtos = await _aberturaDiaServices.ListarAberturasDiasAsync();

            var viewModels = _mapper.Map<IList<AberturaDiaViewModel>>(dtos.DTOs);

            return AdapterView("Index", viewModels, dtos.Message);
        }
    }
}
