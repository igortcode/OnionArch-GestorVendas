using Gestao.Application.DTO.ItemComanda;
using Gestao.Application.Enums;
using Gestao.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MioDeBaoGestao.Controllers
{
    public class ItensComandaController : BasicController
    {
        private IItemComandaServices _itensComandaServices;

        public ItensComandaController(IItemComandaServices itemComandaServices)
        {
            _itensComandaServices = itemComandaServices;
        }

        public async Task<IActionResult> ListItensComandaPartial(int idComanda)
        {
            var itens = await _itensComandaServices.ListarItemComandaPorIdEIdComanda(idComanda);

            AddOnlyErrorsNotification(itens.Message);

            return PartialView("_ListItensComandaPartial", itens.DTOs);
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
