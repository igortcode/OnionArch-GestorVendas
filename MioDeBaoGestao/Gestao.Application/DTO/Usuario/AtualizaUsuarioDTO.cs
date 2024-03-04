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

        [Required(ErrorMessage = "Campo confirmar senha é obrigatório.")]
        [StringLength(255, ErrorMessage = "Senha inválida. Deve conter no mínimo 5 caracteres e no máximo 255.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Campo confirmar nova senha é obrigatório.")]
        [StringLength(255, ErrorMessage = "Senha inválida. Deve conter no mínimo 5 caracteres e no máximo 255.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [DisplayName("Confirmar senha")]
        public string ConfirmaNovaSenha { get; set; }

        [Required(ErrorMessage = "Campo cargo é obrigatório.")]
        public string RoleName { get; set; }
    }
}
