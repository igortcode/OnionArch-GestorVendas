using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MioDeBaoGestao.Models.AberturaDia
{
    public class AberturaDiaViewModel
    {
        public int Id { get; set; }
        public string NmDia { get; set; }
        public bool Status { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Faturamento { get; set; }
        public DateTime DataAbertura { get; set; }
    }
}
