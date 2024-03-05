using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gestao.Application.DTO.Login
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Campo nome de usuário obrigatório!")]
        [DisplayName("Nome de usuário")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Campo senha é obrigatório")]
        [StringLength(255, ErrorMessage = "Senha inválida. Deve conter no mínimo 5 caracteres e no máximo 255.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [DisplayName("Senha")]
        public string Password { get; set; }
    }
}
