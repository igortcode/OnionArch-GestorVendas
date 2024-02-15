using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Gestao.Core.Entidades
{
    public class AberturaDia : Entity
    {
        public string NmDia { get; private set; }
        public DateTime DataAbertura { get; private set; }
        public bool Aberta { get; private set; }
        public decimal? Faturamento { get; private set; }

        public ICollection<Comanda> Comandas { get; set; }

        public AberturaDia()
        {
            NmDia = Enum.GetName<DayOfWeek>(DateTime.Now.DayOfWeek);
            DataAbertura = DateTime.Now;
            Aberta = true;
            Faturamento = null;
        }

        public AberturaDia(int id, string nmDia, DateTime dataAbertura, bool aberta, decimal? faturamento)
        {
            Id = id;
            NmDia = nmDia;
            DataAbertura = dataAbertura;
            Aberta = aberta;
            Faturamento = faturamento;
        }

        public void FecharDia()
        {
            Aberta = false;
            decimal valorTotal = 0;

            foreach (var comanda in Comandas)
            {
                comanda.FecharComanda();
                valorTotal += comanda.ValorTotal();
            }

            Faturamento = valorTotal;
        }

        public void AbrirDia()
        {
            Aberta = true;
        }
    }
}
