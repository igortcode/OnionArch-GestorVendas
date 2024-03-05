using System.ComponentModel;

namespace Gestao.Application.DTO.Usuario
{
    public class ObterUsuarioDTO
    {
        public string Id { get; set; }
        [DisplayName("Nome de usuário")]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        [DisplayName("Cargo")]
        public string RoleName { get; set; }
        public bool Habilitado { get; set; }
    }
}
