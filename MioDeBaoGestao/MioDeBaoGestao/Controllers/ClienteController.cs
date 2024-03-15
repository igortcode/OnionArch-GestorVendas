using Gestao.Application.DTO.Cliente;
using Gestao.Application.Enums;
using Gestao.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MioDeBaoGestao.Controllers
{
    [Authorize]
    public class ClienteController : BasicController
    {
        private readonly IClienteServices _clienteServices;

        public ClienteController(IClienteServices clienteServices)
        {
            _clienteServices = clienteServices;
        }

        public async Task<IActionResult> Index(string message, TipoNotificacao? tipoNotificacao)
        {
            var clientes = await _clienteServices.ListarPaginadoAsync(1,5);

            if(!string.IsNullOrEmpty(message) && tipoNotificacao.HasValue)
            {
                AddNotification(tipoNotificacao.Value, message);
            }
            else
            {
                AddOnlyErrorsNotification(clientes.Message);
            }


            return View(clientes);
        }

        public async Task<IActionResult> Search(string search, int? page)
        {
            var clientes = await _clienteServices.PesquisarPaginadoAsync(search, page ?? 1, 5);

            AddOnlyErrorsNotification(clientes.Message);

            return View("Index", clientes);
        }

        public async Task<IActionResult> ListarClientePartial(string search, int? page)
        {
            var clientes = await _clienteServices.PesquisarPaginadoAsync(search, page ?? 1, 5);

            AddOnlyErrorsNotification(clientes.Message);

            return PartialView("_ListarCliente", clientes);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClienteDTO clienteDTO)
        {

            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage);

                AddNotification(erros.ToArray());

                return View(clienteDTO);
            }

            var result = await _clienteServices.AdicionarAsync(clienteDTO);


            return RedirectToAction(nameof(Index), new {Message = result.Mensagem, TipoNotificacao = result.TpNotif});
        }


        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _clienteServices.BuscarPorIdAsync(id);

            AddOnlyErrorsNotification(cliente.Message);

            if(cliente.DTO is null)
            {
                AddNotification("Não foi encontrado entidade com esse identificador!");

                return RedirectToAction(nameof(Index));
            }

            return View(cliente.DTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ClienteDTO clienteDTO)
        {

            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage);

                AddNotification(erros.ToArray());

                return View(clienteDTO);
            }

            var result = await _clienteServices.AtualizarAsync(id, clienteDTO);


            return RedirectToAction(nameof(Index), new { Message = result.Mensagem, TipoNotificacao = result.TpNotif });
        }


        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _clienteServices.BuscarPorIdAsync(id);

            if (cliente.DTO is null)
            {
                AddNotification("Não foi encontrado entidade com esse identificador!");

                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return Json(cliente.Message.Mensagem);
            }

            var result = await _clienteServices.ExcluirAsync(id);

            AddNotification(result);

            return Json(result.Mensagem);
        }
    }
}
