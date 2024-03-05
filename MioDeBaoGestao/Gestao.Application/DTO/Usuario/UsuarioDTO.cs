using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gestao.Application.DTO.Usuario
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage = "Campo nome de usuário é obrigatório")]
        [DisplayName("Nome de Usuário")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Campo Email é obrigatório")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo senha é obrigatório")]
        [StringLength(255, ErrorMessage = "Senha inválida. Deve conter no mínimo 5 caracteres e no máximo 255.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [DisplayName("Senha")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Campo confirmar senha é obrigatório.")]
        [StringLength(255, ErrorMessage = "Senha inválida. Deve conter no mínimo 5 caracteres e no máximo 255.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Senha")]
        [DisplayName("Confirmar senha")]
        public string ConfirmaSenha { get; set; }

        [Required(ErrorMessage = "Campo cargo é obrigatório.")]
        [DisplayName("Cargo")]
        public string RoleName { get; set; }
    }
}
