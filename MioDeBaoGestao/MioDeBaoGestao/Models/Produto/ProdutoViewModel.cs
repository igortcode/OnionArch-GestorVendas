using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MioDeBaoGestao.Models.Produto
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo {0} obrigatório!")]
        [DisplayName("Nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo {0} obrigatório!")]
        [DisplayName("Preço")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Preco { get; set; }
        [Required(ErrorMessage = "Campo {0} obrigatório!")]
        public int Quantidade { get; set; }
    }
}
