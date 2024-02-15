using Gestao.Core.Validations.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Gestao.Core.Entidades
{
    public class Produto : Entity
    {
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }
        public int Quantidade { get; private set; }

        public ICollection<ItemComanda> ItensComanda { get; set; }

        public Produto(string nome, decimal preco, int quantidade)
        {
            validacaoCampos(nome, preco, quantidade);

            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
        }

        public Produto(int id, string nome, decimal preco, int quantidade)
        {
            Id = id;
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
                throw new DomainExceptionValidate("Não possui essa quantidade para retirada. Atualize as quantidades.");

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

        private void validacaoCampos(string nome, decimal preco, int quantidade)
        {
            DomainExceptionValidate.When(string.IsNullOrWhiteSpace(nome), "Nome inválido. Não pode ser nulo ou vazio.");
            DomainExceptionValidate.When(nome.Length > 255, "Nome inválido. Deve possuir no máximo 255 caracteres.");
            DomainExceptionValidate.When(preco <= 0, "Preco inválido. Deve ser maior ou igual a 0.");
            DomainExceptionValidate.When(quantidade <= 0, "Quantidade inválida. Deve ser maior ou igual a 0.");

        }
    }
}
