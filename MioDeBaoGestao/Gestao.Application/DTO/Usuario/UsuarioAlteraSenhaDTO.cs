using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Gestao.Application.DTO.Usuario
{
    public class UsuarioAlteraSenhaDTO
    {
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
        [Compare("Password")]
        [DisplayName("Confirmar senha")]
        public string ConfirmaSenha { get; set; }
        public string Token { get; set; }
    }
}
