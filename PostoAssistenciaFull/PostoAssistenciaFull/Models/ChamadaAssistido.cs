using System;

namespace PostoAssistenciaFull.Models
{
    public class ChamadaAssistido
    {
        public Guid ChamadaAssistidoId { get; set; }

        public Guid ChamadaId { get; set; }

        public Guid AssistidoId { get; set; }

        public string Observação { get; set; }
    }
}