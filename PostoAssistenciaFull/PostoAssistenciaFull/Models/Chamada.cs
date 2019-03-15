using System;

namespace PostoAssistenciaFull.Models
{
    public class Chamada
    {
        public Guid ChamadaId { get; set; }

        public DateTime DataChamada { get; set; }

        public DateTime DataCriacao { get; set; }

        public string Observação { get; set; }
    }
}