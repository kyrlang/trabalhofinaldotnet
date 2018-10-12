using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Messaging;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WCFTransmissorMensagens.Models;

namespace WCFTransmissorMensagens
{
    public class TransmissorMensagem : ITransmissorMensagem
    {
        public bool InserirMensagem(Pacientes pacientes)
        {
            try
            {
                if (MessageQueue.Exists(ConfigurationManager.AppSettings["nomeFila"])) //verifica se a fila existe
                {
                    var fila = new MessageQueue(ConfigurationManager.AppSettings["caminhoFila"]); //obtem o caminho da fila para criar objeto
                    var mensagem = new Message // preenche a mensagem a ser enviada para fila
                    {
                        Formatter = new XmlMessageFormatter(new Type[] { typeof(String) }),
                        Body = JsonConvert.SerializeObject(pacientes)
                    };

                    fila.Send(mensagem); //envia mensagem para fila
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
