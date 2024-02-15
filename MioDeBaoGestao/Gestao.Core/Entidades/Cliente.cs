using Gestao.Core.Validations.Exceptions;
using System.Collections.Generic;

namespace Gestao.Core.Entidades
{
    public class Cliente : Entity
    {
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public ICollection<Comanda> Comandas { get; set; }

        public Cliente(string nome, string cPF)
        {
            validaCampos(nome, cPF);

            Nome = nome;
            CPF = cPF;
        }

        public Cliente(int id, string nome, string cPF)
        {
            Id = id;
            Nome = nome;
            CPF = cPF;
        }


        private void validaCampos(string nome, string cPF)
        {
            DomainExceptionValidate.When(string.IsNullOrWhiteSpace(nome), "Nome inválido. Não pode ser nulo ou vazio.");
            DomainExceptionValidate.When(nome.Length > 255, "Nome inválido. Deve possuir no máximo 255 caracteres.");
            DomainExceptionValidate.When(string.IsNullOrWhiteSpace(cPF), "CPF inválido. Não pode ser nulo ou vazio.");
            DomainExceptionValidate.When(cPF.Length > 13, "CPF inválido. Deve possuir no máximo 13 caracteres.");
        }
    }
}
