using AutoMapper;
using Gestao.Core.DTO.Produto;
using Gestao.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using MioDeBaoGestao.Models.Produto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MioDeBaoGestao.Controllers
{
    public class ProdutoController : BasicController
    {
        private readonly IProdutoServices _produtoServices;
        private readonly IMapper _mapper;

        public ProdutoController(IProdutoServices produtoServices, IMapper mapper)
        {
            _produtoServices = produtoServices;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            var dtos = await _produtoServices.ListarAsync();

            var viewModel = _mapper.Map<IEnumerable<ProdutoViewModel>>(dtos.DTOs);

            return AdapterView(viewModel, dtos.Message);
        }


        public async Task<ActionResult> Details(int id)
        {
            var dto = await _produtoServices.BuscarPorIdAsync(id);

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

            if(!(result.TpNotif == Gestao.Core.Enums.TipoNotificacao.Sucess))
                return View(produto);

            return RedirectToAction(nameof(Index));

        }

        public async Task<ActionResult> Edit(int id)
        {
            var dto = await _produtoServices.BuscarPorIdAsync(id);

            if(dto.DTO is null)
            {
                AddNotification("Não foi encontrado nenhuma entidade com esse identificador");

                return RedirectToAction(nameof(Index));
            }


            var viewModel = _mapper.Map<ProdutoViewModel>(dto.DTO);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ProdutoViewModel produto)
        {
            if (!ModelState.IsValid)
                return View(produto);

            var dto = _mapper.Map<ProdutoDTO>(produto);

            var result = await _produtoServices.AtualizarAsync(id, dto);

            AddNotification(result);

            if (!(result.TpNotif == Gestao.Core.Enums.TipoNotificacao.Sucess))
                return View(produto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(int id)
        {
            var result = await _produtoServices.ExcluirAsync(id);

            AddNotification(result);

            return RedirectToAction(nameof(Index));
        }
    }
}
