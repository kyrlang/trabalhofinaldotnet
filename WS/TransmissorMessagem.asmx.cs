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
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TransmissorMessagem : System.Web.Services.WebService
    {

        [WebMethod()]
        public bool TransmitirMensagem(Pacientes pacientes)
        {
            TransmissorMensagemClient client = new TransmissorMensagemClient();
            try
            {
                client.InserirMensagem(pacientes);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
