namespace Gestao.Core.Entidades
{
    public class ItemComanda
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        public int ComandaId { get; set; }
        public Comanda Comanda { get; set; }
    }
}
