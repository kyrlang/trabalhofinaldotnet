using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using WS.WCF;

namespace WS
{
    /// <summary>
    /// Summary description for TransmissorMessagem
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class TransmissorMessagem : System.Web.Services.WebService
    {

        //após criar o WS (WebService) é necessário adicionar a referência do WCF criado no projeto anterior para uso
         [WebMethod()]
        public async Task<HttpResponseMessage> TransmitirMensagem(Pacientes pacientes) //esse serviço espera como parametro um objeto do tipo Paciente referente ao objeto criado no WCF
        {

            try
            {
                HttpResponseMessage response = null;
                using (var client = new HttpClient())
                {
                    var uri = new Uri(@"http://localhost/WCFTransmissorMensagem/TransmissorMensagem.svc/InserirMensagem");
                    var json = JsonConvert.SerializeObject(pacientes);
                    var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await client.PostAsync(uri, stringContent);
                    return response;
                }
            }
            catch (Exception)
            {
                throw;
            }


            //TransmissorMensagemClient client = new TransmissorMensagemClient(); // essa classe é referencia do WCF, que pode ser visualizado na linha 6 (WS.WCF)
            //try
            //{
            //    client.InserirMensagem(pacientes); //após instanciar uma nova classe client do WCF, os serviços ficam disponível para uso, nesse caso estamos usando o InserirMensagem
            //    return true;
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }
    }
}
