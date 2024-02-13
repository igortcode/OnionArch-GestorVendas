using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Gestao.Core.Entidades
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }

        public ICollection<ItemComanda> ItensComanda { get; set; }

        public Produto(string nome, decimal preco, int quantidade)
        {
            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
        }


        public void AdicionarQuantidade(int quantidade)
        {
            Quantidade += quantidade; 
        }

        public void RemoverQuantidade(int quantidade)
        {
            if (Quantidade < quantidade)
                throw new InvalidOperationException("Não possui essa quantidade para retirada. Atualize as quantidades.");

            Quantidade -= quantidade;
        }

        public void Atualizar(string nome, decimal preco, int quantidade)
        {
            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
        }

        public static Expression<Func<Produto, bool>> GetExpression()
        {
            Expression<Func<Produto, bool>> expression = null;

            return expression;
        }
    }
}
