using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WCFTransmissorMensagens.Models;

namespace WCFTransmissorMensagens
{
    [ServiceContract]
    public interface ITransmissorMensagem
    {

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "mensagem")]
        bool InserirMensagem(Pacientes pacientes);

    }


}
