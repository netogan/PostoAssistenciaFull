using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostoAssistenciaFull.Models
{
    public class ChamadaTrabalhador
    {
        public Guid ChamadaTrabalhadorId { get; set; }

        public Guid ChamadaId { get; set; }

        public Guid TrabalhadorId { get; set; }

        public string Observação { get; set; }

        [ForeignKey("ChamadaId")]
        public virtual Chamada Chamada { get; set; }

        [ForeignKey("TrabalhadorId")]
        public virtual Trabalhador Trabalhador { get; set; }
    }
}