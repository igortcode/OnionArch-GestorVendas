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

        public async Task<IActionResult> ListItensComandaPaginadoPartial(int idComanda, TipoConsumo consumidor, int? page)
        {
            var itens = await _itensComandaServices.ListarItemComandaPorIdEIdComandaPaginadoAsync(idComanda, page ?? 1, 5);

            var comanda = await _comandaServices.BuscarPorIdAsync(idComanda);

            AddOnlyErrorsNotification(itens.Message);

            var partial = consumidor == TipoConsumo.Desktop ? "_ListItensComandaDesktopPartial" : "_ListItensComandaMobilePartial";

            return PartialView(partial, new ItensComandaViewModel { ItensComandas = itens, ComandaFechada = comanda.DTO.ComandaFechada});
        }

        public async Task<IActionResult> PesquisarItensComandaPaginadoPartial(int idComanda, TipoConsumo consumidor, string search, int? page)
        {
            var itens = await _itensComandaServices.PesquisarItemComandaPorIdEIdComandaPaginadoAsync(search, idComanda, page ?? 1, 5);

            var comanda = await _comandaServices.BuscarPorIdAsync(idComanda);

            AddOnlyErrorsNotification(itens.Message);

            var partial = consumidor == TipoConsumo.Desktop ? "_ListItensComandaDesktopPartial" : "_ListItensComandaMobilePartial";

            var total = await _itensComandaServices.ObterSomatorioValorItemComandaAsync(idComanda);

            ViewData["ValorTotal"] = total.DTO.ToString("n2");

            return PartialView(partial, new ItensComandaViewModel { ItensComandas = itens, ComandaFechada = comanda.DTO.ComandaFechada });
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

            return Json("Cadastro efetuado com sucesso!");

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

            return Json("Cadastro ataulizado com sucesso!"); ;

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
