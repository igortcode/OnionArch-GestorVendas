using System;
using System.ComponentModel;

namespace Gestao.Application.DTO.Comanda
{
    public class ObterComandaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [DisplayName("Status")]
        public bool ComandaFechada { get; set; }
        public decimal? Total { get; set; }
        public int AberturaDiaId { get; set; }
        public int? ClienteId { get; set; }
        [DisplayName("Nome Cliente")]
        public string NmCliente { get; set; }
        public DateTime DtCriacao { get; set; }
    }
}
