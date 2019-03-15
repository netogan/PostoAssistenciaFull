using System;

namespace PostoAssistenciaFull.Models
{
    public class ChamadaTrabalhador
    {
        public Guid ChamadaTrabalhadorId { get; set; }

        public Guid ChamadaId { get; set; }

        public Guid TrabalhadorId { get; set; }

        public string Observação { get; set; }
    }
}