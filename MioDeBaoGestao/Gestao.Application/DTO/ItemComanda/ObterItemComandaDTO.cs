namespace Gestao.Application.DTO.ItemComanda
{
    public class ObterItemComandaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public int ProdutoId { get; set; }
        public string NmProdutoPai { get; set; }
        public int ComandaId { get; set; }
        public string NmComanda { get; set; }
    }
}
