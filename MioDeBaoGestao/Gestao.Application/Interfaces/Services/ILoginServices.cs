using Gestao.Application.DTO.Generic;
using Gestao.Application.DTO.Login;
using System.Threading.Tasks;

namespace Gestao.Application.Interfaces.Services
{
    public interface ILoginServices
    {
        Task<MessageDTO> SignInAsync(LoginDTO dto);
        Task<MessageDTO> LogoutAsync();
    }
}
