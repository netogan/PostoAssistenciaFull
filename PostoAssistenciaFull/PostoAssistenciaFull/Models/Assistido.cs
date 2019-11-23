using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostoAssistenciaFull.Models
{
    public class Assistido
    {
        public Guid AssistidoId { get; set; }

        [Required(ErrorMessage = "Informe o nome completo", AllowEmptyStrings = false)]
        public string NomeCompleto { get; set; }

        public string Sexo { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Informe uma idade válida")]
        public int Idade { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DataNascimento { get; set; }

        public string GrauParentesco { get; set; }

        public int? Telefone1 { get; set; }

        public int? Telefone2 { get; set; }

        public int? NumeracaoCalcado { get; set; }

        public string NumeracaoRoupaSuperior { get; set; }

        public string NumeracaoRoupaInferior { get; set; }

        public string Observacao { get; set; }

        public bool Principal { get; set; }

        public Guid? DependenteId { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public string Cpf { get; set; }

        public string Rg { get; set; }

        public string FotoBase64 { get; set; }

        public virtual Guid? EnderecoId { get; set; }

        [ForeignKey("EnderecoId")]
        public virtual Endereco Endereco { get; set; }
    }
}