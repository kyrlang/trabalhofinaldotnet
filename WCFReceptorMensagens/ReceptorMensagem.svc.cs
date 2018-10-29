using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;
using System.Messaging;
using WCFReceptorMensagens.Model;

namespace WCFReceptorMensagens
{
    public class ReceptorMensagem : IReceptorMensagem
    {
        public void BuscarMensagem()
        {
            if (MessageQueue.Exists(ConfigurationManager.AppSettings["nomeFila"])) //verifica se a fila existe
            {
                using (var fila = new MessageQueue(ConfigurationManager.AppSettings["caminhoFila"])) //pega o caminho da fila através de seu nome configurado no web.config nesse caso de nome "fila" 
                {
                    fila.Formatter = new XmlMessageFormatter(new Type[] { typeof(String) }); //formata a fila pra receber a mensagem em forma de JSON
                    var mensagens = fila.GetAllMessages().ToList(); //pega todas as mensagens na fila
                    foreach (var item in mensagens) //pecorrer a lita de mensagens para pegar o conteúdo de cada uma
                    {
                        var conteudoMensagem = JsonConvert.DeserializeObject(item.Body.ToString()); // obtem o conteúdo da mensagem para inserir no banco
                        Paciente paciente = JsonConvert.DeserializeObject<Paciente>(item.Body.ToString());
                        // Gravar no banco
                        using (var context = new Context())
                        {
                            context.Paciente.Add(paciente);
                            context.SaveChanges();
                        }
                    }

                    fila.Purge(); // exclui a lista de mensagem
                }
            }

            #region Lixo
            //var filas = MessageQueue.GetPrivateQueuesByMachine(Environment.MachineName);
            //var filaTeste = filas[0];
            //filaTeste.BeginPeek();
            //filaTeste.PeekCompleted += new PeekCompletedEventHandler(MessageHasBeenReceived);

            //var filas = MessageQueue.GetPrivateQueuesByMachine(Environment.MachineName);
            //var filaTeste = filas[0];
            //filaTeste.Formatter = new XmlMessageFormatter(new Type[] { typeof(String) });
            //var mensagens = filaTeste.GetAllMessages().ToList();
            //foreach (var item in mensagens)
            //{
            //    var conteudoMensagem = JsonConvert.DeserializeObject(item.Body.ToString());
            //    // Gravar no banco
            //}
            //filaTeste.Purge();
            #endregion

        }

        private void MessageHasBeenReceived(object sender, PeekCompletedEventArgs e)
        {
            var filas = MessageQueue.GetPrivateQueuesByMachine(Environment.MachineName);
            var filaTeste = filas[0];

            // Get message
            var msg = filaTeste.EndPeek(e.AsyncResult);

            // Do message processing here

            // Remove message from queue
            filaTeste.ReceiveById(msg.Id);

            // Listen for more messages
            filaTeste.BeginPeek();
            filaTeste.PeekCompleted += new PeekCompletedEventHandler(MessageHasBeenReceived);
        }

        

    }
}
