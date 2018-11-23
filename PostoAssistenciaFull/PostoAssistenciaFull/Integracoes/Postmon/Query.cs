using Newtonsoft.Json;
using PostoAssistenciaFull.Integracoes.Postmon.Models;
using RestSharp;

namespace PostoAssistenciaFull.Integracoes.Postmon
{
    public class Query
    {
        public CepResponse ConsultarCep(string cep)
        {
            var cliente = new RestClient("https://api.postmon.com.br/v1");

            var request = new RestRequest();

            request.Method = Method.GET;
            request.Resource = $"/cep/{cep}";

            var response = cliente.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return new CepResponse() { EhValido = false };

            var retorno = JsonConvert.DeserializeObject<CepResponse>(response.Content);

            retorno.EhValido = true;

            return retorno;
        }

    }
}