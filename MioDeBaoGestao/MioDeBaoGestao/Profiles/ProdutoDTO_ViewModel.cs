using AutoMapper;
using Gestao.Core.DTO.Produto;
using MioDeBaoGestao.Models.Produto;

namespace MioDeBaoGestao.Profiles
{
    public class ProdutoDTO_ViewModel : Profile
    {
        public ProdutoDTO_ViewModel()
        {
            CreateMap<ProdutoDTO, ProdutoViewModel>().ReverseMap();
            CreateMap<ObterProdutoDto, ProdutoViewModel>();
        }
    }
}
