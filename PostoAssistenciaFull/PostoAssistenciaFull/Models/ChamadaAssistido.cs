using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostoAssistenciaFull.Models
{
    public class ChamadaAssistido
    {
        public Guid ChamadaAssistidoId { get; set; }

        public Guid ChamadaId { get; set; }

        public Guid AssistidoId { get; set; }

        public string Observação { get; set; }

        [ForeignKey("ChamadaId")]
        public virtual Chamada Chamada { get; set; }

        [ForeignKey("AssistidoId")]
        public virtual Assistido Assistido { get; set; }
    }
}