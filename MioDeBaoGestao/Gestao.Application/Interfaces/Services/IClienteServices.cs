using Gestao.Application.DTO.Cliente;
using Gestao.Application.DTO.Generic;

namespace Gestao.Application.Interfaces.Services
{
    public interface IClienteServices : IGenericServices<ClienteDTO, GGet<ObterClienteDTO>, GList<ObterClienteDTO>, MessageDTO> 
    {
    }
}
