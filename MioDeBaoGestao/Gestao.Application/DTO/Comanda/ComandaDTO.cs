using System.ComponentModel.DataAnnotations;

namespace Gestao.Application.DTO.Comanda
{
    public class ComandaDTO
    {
        [Required(ErrorMessage = "Campo {0} obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo identificador da abertura obrigatório!")]
        public int AberturaDiaId { get; set; }
        public int? ClienteId { get; set; }
    }
}
