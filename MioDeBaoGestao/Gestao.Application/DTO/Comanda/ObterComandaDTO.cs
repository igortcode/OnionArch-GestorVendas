namespace Gestao.Application.DTO.Comanda
{
    public class ObterComandaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool ComandaFechada { get; set; }
        public decimal? Total { get; set; }
        public int AberturaDiaId { get; set; }
        public int? ClienteId { get; set; }
        public string NmCliente { get; set; }
    }
}
