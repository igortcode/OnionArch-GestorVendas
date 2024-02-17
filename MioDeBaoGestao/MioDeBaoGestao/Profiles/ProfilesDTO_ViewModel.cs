using AutoMapper;
using Gestao.Application.DTO.Produto;
using MioDeBaoGestao.Models.Produto;

namespace MioDeBaoGestao.Profiles
{
    public class ProfilesDTO_ViewModel : Profile
    {
        public ProfilesDTO_ViewModel()
        {
            CreateMap<ProdutoDTO, ProdutoViewModel>().ReverseMap();
            CreateMap<ObterProdutoDto, ProdutoViewModel>();
        }
    }
}
