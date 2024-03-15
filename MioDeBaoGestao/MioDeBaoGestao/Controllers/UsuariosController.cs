using Gestao.Application.DTO.Usuario;
using Gestao.Application.Enums;
using Gestao.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MioDeBaoGestao.Controllers
{
    [Authorize]
    public class UsuariosController : BasicController
    {
        private readonly IUserServices _userServices;
        private readonly IRoleServices _roleServices;

        public UsuariosController(IUserServices userServices, IRoleServices roleServices)
        {
            _userServices = userServices;
            _roleServices = roleServices;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string message, TipoNotificacao? tipoNotificacao)
        {
            var result = await _userServices.ListarUsuariosPaginadoAsync(1, 5);

            if (!string.IsNullOrWhiteSpace(message))
            {
                AddNotification(tipoNotificacao.Value, message);
            }
            else
            {
                AddOnlyErrorsNotification(result.Message);
            }


            return View(result);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Search(string search, int page)
        {
            var result = await _userServices.PesquisarUsuarioPaginadoAsync(search, page, 5);

            AddOnlyErrorsNotification(result.Message);

            return View("Index", result);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var roles = await _roleServices.ListarCargos();

            AddOnlyErrorsNotification(roles.Message);

            ViewBag.RoleName = new SelectList(roles.DTOs, "Role", "Role");

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(UsuarioDTO dto)
        {
            await PopularViewBag();

            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values
                                       .SelectMany(x => x.Errors)
                                       .Select(x => x.ErrorMessage);

                AddNotification(erros.ToArray());


                return View(dto);
            }

            var result = await _userServices.AddUsuario(dto);

            if (result.TpNotif != TipoNotificacao.Sucess)
            {
                AddNotification(result);

                return View(dto);
            }

            return RedirectToAction(nameof(Index), new { Message = result.Mensagem, TipoNotificacao = result.TpNotif });
        }


        public async Task<IActionResult> Edit()
        {
            var userName = User.Identity.Name;

            var user = await _userServices.ObterUsuarioPorUserNameAsync(userName);

            AddOnlyErrorsNotification(user.Message);

            await PopularViewBag(user.DTO.RoleName);

            var atualizaUsuario = new AtualizaUsuarioDTO
            {
                Id = user.DTO.Id,
                Email = user.DTO.Email,
                RoleName = user.DTO.RoleName,
                UserName = userName,
            };

            return View(atualizaUsuario);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AtualizaUsuarioDTO dto)
        {
            await PopularViewBag(dto.RoleName);

            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values
                                       .SelectMany(x => x.Errors)
                                       .Select(x => x.ErrorMessage);

                AddNotification(erros.ToArray());


                return View(dto);
            }

            var result = await _userServices.AtualizarUsuario(dto);

            if (result.TpNotif != TipoNotificacao.Sucess)
            {
                AddNotification(result);

                return View(dto);
            }

            return RedirectToAction(nameof(Index), "AberturaDia", new { Message = result.Mensagem, TipoNotificacao = result.TpNotif });
        }


        public async Task<IActionResult> AtualizarStatus(string id)
        {
            var result = await _userServices.InativarAtivarUsuario(id);

            if (result.TpNotif != TipoNotificacao.Sucess)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(result.Mensagem);
            }

            return Json(result.Mensagem);
        }

        private async Task PopularViewBag(string role = null)
        {
            var roles = await _roleServices.ListarCargos();
            if (string.IsNullOrEmpty(role))
            {
                ViewBag.RoleName = new SelectList(roles.DTOs, "Role", "Role");
            }
            else
            {
                ViewBag.RoleName = new SelectList(roles.DTOs, "Role", "Role", role);
            }
        }
    }
}
