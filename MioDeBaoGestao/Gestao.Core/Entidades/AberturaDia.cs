using System;
using System.Collections.Generic;

namespace Gestao.Core.Entidades
{
    public class AberturaDia
    {
        public int Id { get; set; }
        public string NmDia { get; set; }
        public DateTime DataAbertura { get; set; }

        public ICollection<Comanda> Comandas { get; set; }

        public AberturaDia()
        {
            NmDia = Enum.GetName<DayOfWeek>(DateTime.Now.DayOfWeek);
            DataAbertura = DateTime.Now;
        }
    }
}
