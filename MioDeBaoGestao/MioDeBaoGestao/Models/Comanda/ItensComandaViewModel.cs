using Gestao.Application.DTO.Generic;
using Gestao.Application.DTO.ItemComanda;

namespace MioDeBaoGestao.Models.Comanda
{
    public class ItensComandaViewModel
    {
        public bool ComandaFechada { get; set; }
        public bool DiaFechado { get; set; }
        public GList<ObterItemComandaDTO> ItensComandas { get; set; }
    }
}
