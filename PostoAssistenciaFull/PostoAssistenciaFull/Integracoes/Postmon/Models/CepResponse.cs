namespace PostoAssistenciaFull.Integracoes.Postmon.Models
{
    public class CepResponse
    {
        public string cidade { get; set; }

        public string bairro { get; set; }

        public string logradouro { get; set; }

        public EstadoInfo estado_info { get; set; }

        public string cep { get; set; }

        public CidadeInfo cidade_info { get; set; }

        public string estado { get; set; }

        public bool EhValido { get; set; }
    }
}