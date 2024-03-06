using Gestao.Core.Validations.Exceptions;
using System;
using System.Collections.Generic;

namespace Gestao.Core.Entidades
{
    public class AberturaDia : Entity
    {
        public string NmDia { get; private set; }
        public DateTime DataAbertura { get; private set; }
        public bool Status { get; private set; }
        public decimal? Faturamento { get; private set; }

        public ICollection<Comanda> Comandas { get; set; }

        public AberturaDia()
        {
            NmDia = BuscarDiaAtual();
            DataAbertura = DateTime.Now;
            Status = true;
            Faturamento = null;
        }

        public AberturaDia(int id, string nmDia, DateTime dataAbertura, bool status, decimal? faturamento)
        {
            Id = id;
            NmDia = nmDia;
            DataAbertura = dataAbertura;
            Status = status;
            Faturamento = faturamento;
        }

        public void FecharDia()
        {
            Status = false;
            decimal valorTotal = 0;

            foreach (var comanda in Comandas)
            {
                comanda.FecharComanda();
                comanda.CalculaValorTotal();

                valorTotal += comanda.Total.Value;
            }

            Faturamento = valorTotal;
        }

        public void AbrirDia()
        {
            Status = true;
        }

        private string BuscarDiaAtual()
        {
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "Segunda-Feira";
                case DayOfWeek.Tuesday:
                    return "Terça-Feira";
                case DayOfWeek.Wednesday:
                    return "Quarta-Feira";
                case DayOfWeek.Thursday:
                    return "Quinta-Feira";
                case DayOfWeek.Friday:
                    return "Sexta-Feira";
                case DayOfWeek.Saturday:
                    return "Sabado";
                case DayOfWeek.Sunday:
                    return "Domingo";
                default:
                    throw new DomainExceptionValidate("Dia inválido");
            }
        }
    }
}
