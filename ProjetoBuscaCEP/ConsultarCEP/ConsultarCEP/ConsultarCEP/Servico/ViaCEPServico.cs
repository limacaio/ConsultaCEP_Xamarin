using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using ConsultarCEP.Servico.Models;
using Newtonsoft.Json;

namespace ConsultarCEP.Servico
{
    //classe utilizada para fazer o download das informações (Json) do site viaCep.com.br
    public class ViaCEPServico
    {
        private static string EnderecoURL = "http://viacep.com.br/ws/{0}/json/";


        public static Endereco BuscarEnderecoViaCEP(string cep)
        {
            //reconstruindo a url com o cep informado pelo usuario
            string NovoEnderecoURL = string.Format(EnderecoURL, cep);

            WebClient wc = new WebClient();
            string conteudo = wc.DownloadString(NovoEnderecoURL);

            //desserializando o arquivo json convertendo o conteudo em um obj do tipo endereço
            Endereco end = JsonConvert.DeserializeObject<Endereco>(conteudo);

            if (end.cep == null) return null;

            return end;
        }
    }
}
