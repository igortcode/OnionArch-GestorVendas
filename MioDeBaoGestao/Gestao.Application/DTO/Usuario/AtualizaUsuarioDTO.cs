using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Gestao.Application.DTO.Usuario
{
    public class AtualizaUsuarioDTO
    {
        [Required(ErrorMessage = "Campo Id é obrigatório")]
        public string Id { get; set; }

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

        [DataType(DataType.Password)]
        [DisplayName("Nova senha")]
        public string NovaSenha { get; set; }

        [DataType(DataType.Password)]
        [Compare("NovaSenha")]
        [DisplayName("Confirmar senha")]
        public string ConfirmaNovaSenha { get; set; }

        [Required(ErrorMessage = "Campo cargo é obrigatório.")]
        [DisplayName("Cargo")]
        public string RoleName { get; set; }
    }
}
