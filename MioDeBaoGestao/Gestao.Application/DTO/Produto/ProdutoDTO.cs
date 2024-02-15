using System.ComponentModel.DataAnnotations;

namespace Gestao.Application.DTO.Produto
{
    public class ProdutoDTO
    {
        [Required(ErrorMessage = "Campo {0} obrigatório!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo {0} obrigatório!")]
        public decimal Preco { get; set; }
        [Required(ErrorMessage = "Campo {0} obrigatório!")]
        [MinLength(1, ErrorMessage = "Quantidade minima é 1.")]
        public int Quantidade { get; set; }
    }
}
