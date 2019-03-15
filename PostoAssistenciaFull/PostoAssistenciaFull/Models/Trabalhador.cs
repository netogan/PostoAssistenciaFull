using System;
using System.ComponentModel.DataAnnotations;

namespace PostoAssistenciaFull.Models
{
    public class Trabalhador
    {
        public Guid TrabalhadorId { get; set; }

        [Required(ErrorMessage = "Informe o nome completo", AllowEmptyStrings = false)]
        public string NomeCompleto { get; set; }

        public string Sexo { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Informe uma idade válida")]
        public int Idade { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DataNascimento { get; set; }

        public int? Telefone1 { get; set; }

        public int? Telefone2 { get; set; }

        public DateTime? DataAtualizacao { get; set; }
    }
}