using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Gestao.Core.Entidades
{
    public class AberturaDia
    {
        public int Id { get; set; }
        public string NmDia { get; set; }
        public DateTime DataAbertura { get; set; }
        public bool Aberta { get; set; }
        public decimal? Faturamento { get; set; }

        public ICollection<Comanda> Comandas { get; set; }

        public AberturaDia()
        {
            NmDia = Enum.GetName<DayOfWeek>(DateTime.Now.DayOfWeek);
            DataAbertura = DateTime.Now;
            Aberta = true;
            Faturamento = null;
        }

        public void FecharDia()
        {
            Aberta = false;
            decimal valorTotal = 0;

            foreach (var comanda in Comandas)
            {
                comanda.ComandaFechada = true;
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
