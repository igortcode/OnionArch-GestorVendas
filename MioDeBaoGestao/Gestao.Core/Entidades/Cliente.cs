using Gestao.Core.Validations.Exceptions;
using Gestao.Core.ValuableObjects;
using System.Collections.Generic;

namespace Gestao.Core.Entidades
{
    public class Cliente : Entity
    {
        public string Nome { get; private set; }
        public CpfVO CPF { get; private set; }
        public ICollection<Comanda> Comandas { get; set; }


        public Cliente() { }
        public Cliente(string nome, CpfVO cPF)
        {
            validaCampos(nome);

            Nome = nome;
            CPF = cPF;
        }

        public Cliente(string nome)
        {
            validaCampos(nome);

            Nome = nome;
        }


        public Cliente(int id, string nome, CpfVO cpf)
        {
            Id = id;
            Nome = nome;
            CPF = cpf;
        }

        public void Atualizar(string nome, CpfVO cPF)
        {
            validaCampos(nome);

            Nome = nome;
            CPF = cPF;
        }


        private void validaCampos(string nome)
        {
            DomainExceptionValidate.When(string.IsNullOrWhiteSpace(nome), "Nome inválido. Não pode ser nulo ou vazio.");
            DomainExceptionValidate.When(nome.Length > 255, "Nome inválido. Deve possuir no máximo 255 caracteres.");
        }
    }
}
