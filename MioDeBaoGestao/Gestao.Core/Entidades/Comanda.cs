using Gestao.Core.Validations.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gestao.Core.Entidades
{
    public class Comanda : Entity
    {
        public string Nome { get; private set; }
        public bool ComandaFechada { get; private set; }
        public decimal? Total { get; private set; }
        public int AberturaDiaId { get; private set; }
        public AberturaDia AberturaDia { get; set; }
        public int? ClienteId { get; private set; }
        public Cliente? Cliente { get; set; }
        public ICollection<ItemComanda> Itens { get; set; }

        public Comanda(string nome, int aberturaDiaId, int? clienteId = null)
        {
            validaCampos(nome, aberturaDiaId, clienteId);

            Nome = nome;
            AberturaDiaId = aberturaDiaId;
            ClienteId = clienteId;
        }

        public Comanda(int id, string nome, bool comandaFechada, decimal? total, int aberturaDiaId,  int? clienteId)
        {
            Id = id;
            Nome = nome;
            AberturaDiaId = aberturaDiaId;
            ClienteId = clienteId;
            ComandaFechada = comandaFechada;
            Total = total;
        }


        public decimal ValorTotal()
        {
            CalculaValorTotal();

            return Total.Value;
        }

        public void FecharComanda()
        {
            ComandaFechada = true;
        }

        private void CalculaValorTotal()
        {
            if (Itens != null && Itens.Count > 0)
            {
                Total = Itens.Sum(a => a.Preco);
            }
            else
            {
                Total = 0;
            }

        }

        private void validaCampos(string nome, int aberturaDiaId, int? clienteId)
        {
            DomainExceptionValidate.When(string.IsNullOrWhiteSpace(nome), "Nome inválido. Não pode ser nulo ou vazio.");
            DomainExceptionValidate.When(nome.Length > 255, "Nome inválido. Deve possuir no máximo 255 caracteres.");
            DomainExceptionValidate.When(aberturaDiaId <= 0, "Dia de abertura inválido. Deve ser maior do que 0");
            if(clienteId.HasValue)
                DomainExceptionValidate.When(clienteId.Value <= 0, "Id do cliente inválido. Deve ser maior do que 0");
        }
    }
}
