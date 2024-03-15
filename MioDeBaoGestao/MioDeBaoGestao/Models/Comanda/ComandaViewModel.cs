using Gestao.Application.DTO.Comanda;
using Gestao.Application.DTO.Generic;
using Gestao.Application.DTO.ItemComanda;
using System.Collections.Generic;

namespace MioDeBaoGestao.Models.Comanda
{
    public class ComandaViewModel
    {
        public ObterComandaDTO Comanda { get; set; }

        public GList<ObterItemComandaDTO> Itens { get; set; }
    }
}
