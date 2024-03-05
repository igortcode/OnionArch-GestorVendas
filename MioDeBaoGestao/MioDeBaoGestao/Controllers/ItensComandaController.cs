using Gestao.Application.DTO.ItemComanda;
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
    public class ItensComandaController : BasicController
    {
        private readonly IItemComandaServices _itensComandaServices;
        private readonly IComandaServices _comandaServices;

        public ItensComandaController(IItemComandaServices itemComandaServices, IComandaServices comandaServices)
        {
            _itensComandaServices = itemComandaServices;
            _comandaServices = comandaServices;
        }

        public async Task<IActionResult> ListItensComandaPartial(int idComanda)
        {
            var itens = await _itensComandaServices.ListarItemComandaPorIdEIdComanda(idComanda);

            var comanda = await _comandaServices.BuscarPorIdAsync(idComanda);

            AddOnlyErrorsNotification(itens.Message);

            return PartialView("_ListItensComandaPartial", new ItensComandaViewModel { ItensComandas = itens.DTOs, ComandaFechada = comanda.DTO.ComandaFechada});
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ItemComandaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage);

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(string.Join(", ", erros));
            }

            var result = await _itensComandaServices.AdicionarAsync(dto);

            if (result.TpNotif != TipoNotificacao.Sucess)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(result.Mensagem);
            }

            return await ListItensComandaPartial(dto.ComandaId);

        }

        public async Task<IActionResult> Get(int id, int idComanda)
        {

            var result = await _itensComandaServices.ObterItemComandaPorIdEIdComanda(id, idComanda);

            if(result.Message.TpNotif != TipoNotificacao.Sucess)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(result.Message.Mensagem);
            }

            return Json(result.DTO);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ItemComandaDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage);

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(string.Join(", ", erros));
            }

            var result = await _itensComandaServices.AtualizarAsync(id, dto);

            if (result.TpNotif != TipoNotificacao.Sucess)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(result.Mensagem);
            }

            return await ListItensComandaPartial(dto.ComandaId);

        }

        public async Task<IActionResult> Delete(int idComanda, int idItemComanda,  int quantidade)
        {

            var result = await _itensComandaServices.ExcluirItemComandaPorIdEIdComanda(idItemComanda, idComanda, quantidade);

            if (result.TpNotif != TipoNotificacao.Sucess)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(result.Mensagem);
            }

            return Json(result.Mensagem);

        }
    }
}
