using Gestao.Application.DTO.ItemComanda;
using System.Collections.Generic;

namespace MioDeBaoGestao.Models.Comanda
{
    public class ItensComandaViewModel
    {
        public bool ComandaFechada { get; set; }
        public IEnumerable<ObterItemComandaDTO> ItensComandas { get; set; }
    }
}
