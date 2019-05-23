using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostoAssistenciaFull.ViewModel.ChamadaTrabalhador
{
    public class ViewModelChamadaTrabalhador
    {
        public Guid ChamadaId { get; set; }
        public Guid[] Trabalhadores { get; set; }
    }
}