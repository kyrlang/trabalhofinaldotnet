using System;
using System.Collections.Generic;
using System.Linq;
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

        //após criar o WS (WebService) é necessário realizar adicionar a referência do WCF criado no projeto anterior para uso
         [WebMethod()]
        public bool TransmitirMensagem(Pacientes pacientes) //esse serviço espera como parametro um objeto do tipo Paciente referente ao objeto criado no WCF
        {
            TransmissorMensagemClient client = new TransmissorMensagemClient(); // essa classe é referencia do WCF, que pode ser visualizado na linha 6 (WS.WCF)
            try
            {
                client.InserirMensagem(pacientes); //após instanciar uma nova classe client do WCF, os serviços ficam disponível para uso, nesse caso estamos usando o InserirMensagem
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
