using AutoMapper;
using Gestao.Application.DTO.AberturaDia;
using Gestao.Application.DTO.Produto;
using MioDeBaoGestao.Models.AberturaDia;
using MioDeBaoGestao.Models.Produto;

namespace MioDeBaoGestao.Profiles
{
    public class ProfilesDTO_ViewModel : Profile
    {
        public ProfilesDTO_ViewModel()
        {
            #region ProdutoVM
            CreateMap<ProdutoDTO, ProdutoViewModel>().ReverseMap();
            CreateMap<ObterProdutoDto, ProdutoViewModel>();
            #endregion

            #region AberturaDia
            CreateMap<AberturaDiaDTO, AberturaDiaViewModel>();
            #endregion

        }
    }
}
