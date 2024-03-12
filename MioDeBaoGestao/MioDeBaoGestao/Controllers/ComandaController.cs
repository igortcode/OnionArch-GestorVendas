using AutoMapper;
using Gestao.Application.DTO.Comanda;
using Gestao.Application.Enums;
using Gestao.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MioDeBaoGestao.Models.Comanda;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MioDeBaoGestao.Controllers
{
    [Authorize]
    public class ComandaController : BasicController
    {
        private readonly IComandaServices _comandaServices;
        private readonly IItemComandaServices _itemComandaServices;
        private readonly IMapper _mapper;
        private readonly IAberturaDiaServices _aberturaDiaServices;

        public ComandaController(IComandaServices comandaServices, IItemComandaServices itemComandaServices, IAberturaDiaServices aberturaDiaServices, IMapper mapper)
        {
            _comandaServices = comandaServices;
            _itemComandaServices = itemComandaServices;
            _mapper = mapper;
            _aberturaDiaServices = aberturaDiaServices;
        }

        public async Task<IActionResult> Index(int idAberturaDia, string message, TipoNotificacao? tipoNotificacao, int? page)
        {
            if (idAberturaDia <= 0)
                return RedirectToAction(nameof(Index), "AberturaDia", new { Message = "Identificador da Abertura do dia inválido.", TipoNotificacao = TipoNotificacao.Alert });

            var comandas = await _comandaServices.ListarPaginadoPorAberturaDiaAsync(idAberturaDia, page ?? 1, 5);


            if (!string.IsNullOrEmpty(message) && tipoNotificacao.HasValue)
            {
                AddNotification(tipoNotificacao.Value, message);
            }
            else
            {
                AddOnlyErrorsNotification(comandas.Message);
            }

            var aberturaDia = await _aberturaDiaServices.BuscarAberturaDiaPorId(idAberturaDia);

            ViewData["StatusDia"] = aberturaDia.DTO.Status;

            ViewData["IdAberturaDia"] = idAberturaDia;


            return View(comandas);
        }

        public async Task<IActionResult> Search(int idAberturaDia, int? page, string search)
        {
            if (idAberturaDia <= 0)
                return RedirectToAction(nameof(Index), "AberturaDia", new { Message = "Identificador da Abertura do dia inválido.", TipoNotificacao = TipoNotificacao.Alert });

            var comandas = await _comandaServices.PesquisarPaginadoPorAberturaDiaAsync(idAberturaDia, search, page ?? 1, 5);

            AddOnlyErrorsNotification(comandas.Message);

            var aberturaDia = await _aberturaDiaServices.BuscarAberturaDiaPorId(idAberturaDia);

            ViewData["StatusDia"] = aberturaDia.DTO.Status;

            ViewData["IdAberturaDia"] = idAberturaDia;

            return View("Index", comandas);
        }

        public IActionResult Create(int idAberturaDia)
        {
            var result = _comandaServices.BuscarUltimoIdRegistrado();

            AddOnlyErrorsNotification(result.Message);

            ViewData["IdAberturaDia"] = idAberturaDia;
            ViewData["SugNome"] = "Comanda " + (++result.DTO);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ComandaDTO comanda)
        {

            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage);

                AddNotification(erros.ToArray());

                return View(comanda);
            }

            var result = await _comandaServices.AdicionarAsync(comanda);


            return RedirectToAction(nameof(Index), new { IdAberturaDia = comanda.AberturaDiaId, Message = result.Mensagem, TipoNotificacao = result.TpNotif });
        }

        public async Task<IActionResult> Edit(int id, int idAberturaDia)
        {
            var comanda = await _comandaServices.BuscarPorIdEAberturadiaAsync(id, idAberturaDia);

            AddOnlyErrorsNotification(comanda.Message);

            if (comanda.DTO is null)
            {
                return RedirectToAction(nameof(Index), new { IdAberturaDia = idAberturaDia, Message = "Não foi encontrado entidade com esse identificador!", TipoNotificacao = TipoNotificacao.Alert });
            }

            return View(comanda.DTO);
        }

        public async Task<IActionResult> Detail(int id, int idAberturaDia)
        {
            var comanda = await _comandaServices.BuscarPorIdEAberturadiaAsync(id, idAberturaDia);

            AddOnlyErrorsNotification(comanda.Message);

            if (comanda.DTO is null)
            {
                return RedirectToAction(nameof(Index), new { IdAberturaDia = idAberturaDia, Message = "Não foi encontrado entidade com esse identificador!", TipoNotificacao = TipoNotificacao.Alert });
            }

            var itens = await _itemComandaServices.ListarItemComandaPorIdEIdComanda(id);

            AddOnlyErrorsNotification(itens.Message);

            var viewModel = new ComandaViewModel
            {
                Comanda = comanda.DTO,
                Itens = itens.DTOs
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ComandaDTO comanda)
        {

            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage);

                AddNotification(erros.ToArray());

                return View(comanda);
            }

            var result = await _comandaServices.AtualizarAsync(id, comanda);


            return RedirectToAction(nameof(Index), new { IdAberturaDia = comanda.AberturaDiaId, Message = result.Mensagem, TipoNotificacao = result.TpNotif });
        }


        public async Task<IActionResult> Delete(int id, int idAberturaDia)
        {
            var result = await _comandaServices.ExcluirAsync(id, idAberturaDia);

            if (result.TpNotif != TipoNotificacao.Sucess)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(result.Mensagem);
            }

            return Json(result.Mensagem);
        }

        public async Task<IActionResult> FecharComanda(int id, int idAberturaDia)
        {
            var result = await _comandaServices.FecharComanda(id, idAberturaDia);

            if (result.TpNotif != TipoNotificacao.Sucess)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(result.Mensagem);
            }

            return Json(result.Mensagem);
        }

    }
}
