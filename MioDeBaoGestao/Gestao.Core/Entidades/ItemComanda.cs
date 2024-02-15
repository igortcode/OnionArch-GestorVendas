using Gestao.Core.Validations.Exceptions;
using System;

namespace Gestao.Core.Entidades
{
    public class ItemComanda : Entity
    {
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }
        public int Quantidade { get; private set; }

        public int ProdutoId { get; private set; }
        public Produto Produto { get; set; }

        public int ComandaId { get; private set; }
        public Comanda Comanda { get; set; }


        public ItemComanda(int id, string nome, decimal preco, int quantidade, int produtoId, int comandaId)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
            ProdutoId = produtoId;
            ComandaId = comandaId;
        }

        public ItemComanda(string nome, decimal preco, int quantidade, int produtoId, int comandaId)
        {
            validaCampos(nome, preco, quantidade, produtoId, comandaId);

            Nome = nome;
            Preco = preco;
            Quantidade = quantidade;
            ProdutoId = produtoId;
            ComandaId = comandaId;
        }

        private void validaCampos(string nome, decimal preco, int quantidade, int produtoId, int comandaId)
        {
            DomainExceptionValidate.When(string.IsNullOrWhiteSpace(nome), "Nome inválido. Não pode ser nulo ou vazio.");
            DomainExceptionValidate.When(nome.Length > 255, "Nome inválido. Deve possuir no máximo 255 caracteres.");
            DomainExceptionValidate.When(preco <= 0, "Preco inválido. Deve ser maior ou igual a 0.");
            DomainExceptionValidate.When(quantidade <= 0, "Quantidade inválida. Deve ser maior ou igual a 0.");
            DomainExceptionValidate.When(produtoId <= 0, "Identificador do produto inválido. Deve ser maior ou igual a 0.");
            DomainExceptionValidate.When(comandaId <= 0, "Identificador da comanda inválido. Deve ser maior ou igual a 0.");
        }
    }
}
