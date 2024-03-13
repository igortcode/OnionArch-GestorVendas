using AutoMapper;
using Gestao.Application.DTO.Produto;
using Gestao.Application.Enums;
using Gestao.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MioDeBaoGestao.Models.Produto;
using System.Linq;
using System.Threading.Tasks;

namespace MioDeBaoGestao.Controllers
{
    [Authorize]
    public class ProdutoController : BasicController
    {
        private readonly IProdutoServices _produtoServices;
        private readonly IMapper _mapper;

        public ProdutoController(IProdutoServices produtoServices, IMapper mapper)
        {
            _produtoServices = produtoServices;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index(string message, TipoNotificacao? tipoNotificacao)
        {
            var dtos = await _produtoServices.ListarPaginadoAsync(1, 5);

            if (!string.IsNullOrEmpty(message) && tipoNotificacao.HasValue)
            {
                AddNotification(tipoNotificacao.Value, message);
            }
            else
            {
                AddOnlyErrorsNotification(dtos.Message);
            }

            return AdapterView(dtos, dtos.Message);
        }

        public async Task<ActionResult> Search(string search, int page)
        {
            var dtos = await _produtoServices.PesquisarPaginadoAsync(search, page, 5);

            ViewData["search"] = search;

            AddOnlyErrorsNotification(dtos.Message);

            return View("Index", dtos);
        }

        public async Task<ActionResult> PesquisarProdutoPartial(string search, int? page)
        {
            var dtos = await _produtoServices.PesquisarProdutoQuantidadePositivaPaginadoAsync(search, page ?? 1, 5);

            AddOnlyErrorsNotification(dtos.Message);

            return PartialView("_ListProdutosPartial", dtos);
        }


        public async Task<ActionResult> Details(int id)
        {
            var dto = await _produtoServices.BuscarPorIdAsync(id);

            if(dto.Message.TpNotif != TipoNotificacao.Sucess)
            {
                return RedirectToAction("Index", new { message = dto.Message.Mensagem, tipoNotificacao = dto.Message.TpNotif });
            }

            var viewModel = _mapper.Map<ProdutoViewModel>(dto.DTO);

            return AdapterView(viewModel, dto.Message);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProdutoViewModel produto)
        {
            if (!ModelState.IsValid)
                return View(produto);

            var dto = _mapper.Map<ProdutoDTO>(produto);

            var result = await _produtoServices.AdicionarAsync(dto);

            AddNotification(result);

            if (result.TpNotif != TipoNotificacao.Sucess)
            {
                var erros = ModelState.Values
                                       .SelectMany(x => x.Errors)
                                       .Select(x => x.ErrorMessage);

                AddNotification(erros.ToArray());
                return View(produto);
            }

            return RedirectToAction(nameof(Index), new { message = result.Mensagem, tipoNotificacao = result.TpNotif });

        }

        public async Task<ActionResult> Edit(int id)
        {
            var dto = await _produtoServices.BuscarPorIdAsync(id);

            if (dto.DTO is null)
            {
                return RedirectToAction(nameof(Index), new { message = "Não foi encontrado nenhuma entidade com esse identificador", tipoNotificacao = dto.Message.TpNotif });
            }

            var viewModel = _mapper.Map<ProdutoViewModel>(dto.DTO);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ProdutoViewModel produto)
        {
            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values
                                    .SelectMany(x => x.Errors)
                                    .Select(x => x.ErrorMessage);

                AddNotification(erros.ToArray());
                return View(produto);
            }

            var dto = _mapper.Map<ProdutoDTO>(produto);

            var result = await _produtoServices.AtualizarAsync(id, dto);

            return RedirectToAction(nameof(Index), new { message = result.Mensagem, tipoNotificacao = result.TpNotif });
        }

        public async Task<ActionResult> Delete(int id)
        {
            var result = await _produtoServices.ExcluirAsync(id);

            return RedirectToAction(nameof(Index), new { message = result.Mensagem, tipoNotificacao = result.TpNotif });
        }
    }
}
