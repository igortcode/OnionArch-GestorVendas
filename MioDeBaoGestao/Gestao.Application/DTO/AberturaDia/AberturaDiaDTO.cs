using System;

namespace Gestao.Application.DTO.AberturaDia
{
    public class AberturaDiaDTO
    {
        public int Id { get; set; }
        public string NmDia { get; set; }
        public bool Aberta { get; set; }
        public decimal? Faturamento { get; set; }
        public DateTime DataAbertura { get; set; }
    }
}
