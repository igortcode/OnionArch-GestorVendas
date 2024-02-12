using System.Collections.Generic;
using System.Linq;

namespace Gestao.Core.Entidades
{
    public class Comanda
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool ComandaFechada { get; set; }
        public int AberturaDiaId { get; set; }
        public AberturaDia AberturaDia { get; set; }
        public int? ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public ICollection<ItemComanda> Itens { get; set; }

        public Comanda(string nome, int aberturaDiaId,  int? clienteId = null)
        {
            Nome = nome;
            AberturaDiaId = aberturaDiaId;
            ClienteId = clienteId;
        }

        public Comanda(int id, string nome, int aberturaDiaId, int? clienteId = null)
        {
            Id = id;
            Nome = nome;
            AberturaDiaId = aberturaDiaId;
            ClienteId = clienteId;
        }


        public decimal ValorTotal()
        {
            return Itens.Sum(a => a.Preco);
        }
    }
}
