using Gestao.Application.DTO.Login;
using Gestao.Application.Enums;
using Gestao.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MioDeBaoGestao.Controllers
{
    public class LoginController : BasicController
    {
        private readonly ILoginServices _loginServices;

        public LoginController(ILoginServices loginServices)
        {
            _loginServices = loginServices;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin") || User.IsInRole("Gerente"))
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (User.IsInRole("Vendedor"))
                {
                    return RedirectToAction("Index", "AberturaDia");
                }
            }


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO login)
        {

            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage);

                AddNotification(erros.ToArray());

                return View(login);
            }

            var result = await _loginServices.SignInAsync(login);

            if (result.TpNotif != TipoNotificacao.Sucess)
            {
                AddNotification(result);

                return View(login);
            }

            if (User.IsInRole("Admin") || User.IsInRole("Gerente"))
                return RedirectToAction("Index", "Home");

            return RedirectToAction("Index", "AberturaDia"); ;
        }


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var result = await _loginServices.LogoutAsync();

            AddNotification(result);

            return RedirectToAction("Login", "Login");
        }
    }
}
