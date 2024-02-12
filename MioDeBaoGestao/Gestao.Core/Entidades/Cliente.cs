using System.Collections;
using System.Collections.Generic;

namespace Gestao.Core.Entidades
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }

        public ICollection<Comanda> Comandas { get; set; }
    }
}
