using AutoMapper;
using Gestao.Core.DTO.Produto;
using Gestao.Core.Entidades;

namespace MioDeBaoGestao.Profiles
{
    public class ProfilesDTO_Entity : Profile
    {
       

        public ProfilesDTO_Entity()
        {
            #region Produto
            CreateMap<Produto, ObterProdutoDto>();
            #endregion
        }


    }
}
