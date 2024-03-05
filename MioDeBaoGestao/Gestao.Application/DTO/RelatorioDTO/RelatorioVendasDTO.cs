using System.ComponentModel;

namespace Gestao.Application.DTO.RelatorioDTO
{
    public class RelatorioVendasDTO
    {
        [DisplayName("Mês")]
        public string Mes { get; set; }
        [DisplayName("Ano")]
        public int Ano { get; set; }
        [DisplayName("Dias trabalhados")]
        public long DiasTrabalhados { get; set; }
        [DisplayName("Valor total")]
        public decimal ValorTotal { get; set; }
    }
}
