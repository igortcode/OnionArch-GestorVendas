using Gestao.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MioDeBaoGestao.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MioDeBaoGestao.Controllers
{

    [Authorize(Roles = "Admin,Gerente")]
    public class HomeController : BasicController
    {
        private readonly IAberturaDiaServices _aberturaDiaServices;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IAberturaDiaServices aberturaDiaServices)
        {
            _aberturaDiaServices = aberturaDiaServices;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _aberturaDiaServices.RelatorioVendasAsync();

            AddOnlyErrorsNotification(result.Message);

            return View(result.DTOs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
