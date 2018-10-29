using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Messaging;
using WCFTransmissorMensagens.Models;

namespace WCFTransmissorMensagens
{
    public class TransmissorMensagem : ITransmissorMensagem
    {
        public Pacientes InserirMensagem(Pacientes pacientes)
        {
            try
            {
                if (MessageQueue.Exists(ConfigurationManager.AppSettings["nomeFila"])) //verifica se a fila existe
                {
                    var fila = new MessageQueue(ConfigurationManager.AppSettings["nomeFila"]); //obtem o caminho da fila para criar objeto
                    var mensagem = new Message // preenche a mensagem a ser enviada para fila
                    {
                        Formatter = new XmlMessageFormatter(new Type[] { typeof(String) }),
                        Body = JsonConvert.SerializeObject(pacientes)
                    };

                    fila.Send(mensagem); //envia mensagem para fila
                    return pacientes;
                }
                else
                {
                    throw new Exception(string.Format("Não foi encontrada uma fila com o nome {0}", ConfigurationManager.AppSettings["nomeFila"]));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
