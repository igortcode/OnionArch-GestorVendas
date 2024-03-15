using Gestao.Application.DTO.Generic;
using Gestao.Application.DTO.Usuario;
using System.Threading.Tasks;

namespace Gestao.Application.Interfaces.Services
{
    public interface IUserServices
    {
        Task<MessageDTO> AddUsuario(UsuarioDTO dto);
        Task<MessageDTO> AtualizarUsuario(AtualizaUsuarioDTO dto);
        Task<MessageDTO> InativarAtivarUsuario(string id);
        Task<GGet<ObterUsuarioDTO>> ObterUsuarioPorIdAsync(string id);
        Task<GGet<ObterUsuarioDTO>> ObterUsuarioPorUserNameAsync(string userName);
        Task<GGet<ObterUsuarioDTO>> ObterUsuarioPorEmail(string email);
        Task<GList<ObterUsuarioDTO>> ListarUsuarios();
        Task<GList<ObterUsuarioDTO>> ListarUsuariosPaginadoAsync(int page, int pageSize);
        Task<GList<ObterUsuarioDTO>> PesquisarUsuarioPaginadoAsync(string search, int page, int pageSize);
        Task<GGet<string>> ObterTokenEsqueciSenha(string email);
        Task<MessageDTO> AlterarSenhaComTokenSenha(UsuarioAlteraSenhaDTO dto);
        Task<MessageDTO> ResetarSenha(UsuarioAlteraSenhaDTO dto);
    }
}
