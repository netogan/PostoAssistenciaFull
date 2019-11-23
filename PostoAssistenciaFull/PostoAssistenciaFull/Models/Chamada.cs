using System;
using System.ComponentModel.DataAnnotations;

namespace PostoAssistenciaFull.Models
{
    public class Chamada
    {
        public Guid ChamadaId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataChamada { get; set; }

        public DateTime DataCriacao { get; set; }

        public string Observação { get; set; }
    }
}