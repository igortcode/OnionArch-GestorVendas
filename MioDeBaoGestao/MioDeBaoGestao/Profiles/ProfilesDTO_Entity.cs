using AutoMapper;
using Gestao.Application.DTO.AberturaDia;
using Gestao.Application.DTO.Cliente;
using Gestao.Application.DTO.Comanda;
using Gestao.Application.DTO.ItemComanda;
using Gestao.Application.DTO.Produto;
using Gestao.Core.Entidades;
using Gestao.Core.ValuableObjects;

namespace MioDeBaoGestao.Profiles
{
    public class ProfilesDTO_Entity : Profile
    {
       

        public ProfilesDTO_Entity()
        {
            #region AberturaDia
            CreateMap<AberturaDia, AberturaDiaDTO>().ReverseMap();
            #endregion

            #region Cliente
            CreateMap<ClienteDTO, Cliente>()
                .ConstructUsing(a => new Cliente(a.Nome, new CpfVO(a.Cpf)));

            CreateMap<Cliente, ObterClienteDTO>()
                .ForMember(a => a.Cpf, op => op.MapFrom(o => o.CPF.Value));
            #endregion

            #region Comanda
            CreateMap<ComandaDTO, Comanda>()
                .ConstructUsing(a => new Comanda(a.Nome, a.AberturaDiaId, a.ClienteId));

            CreateMap<Comanda, ObterClienteDTO>();
            #endregion

            #region Produto
            CreateMap<ProdutoDTO, Produto>()
                .ConstructUsing(a => new Produto(a.Nome, a.Preco, a.Quantidade));

            CreateMap<Produto, ObterProdutoDto>();
            #endregion

            #region ItemComanda
            CreateMap<ItemComanda, ObterItemComandaDTO>()
                .ForMember(a => a.NmComanda, op => op.MapFrom(o => o.Comanda.Nome))
                .ForMember(a => a.NmProdutoPai, op => op.MapFrom(o => o.Produto.Nome));
            #endregion






        }


    }
}
