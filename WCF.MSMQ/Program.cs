using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Messaging;
using System.Threading;
using WCF.MSMQ.Model;

namespace WCF.MSMQ
{
    class Program
    {
        static ManualResetEvent signal = new ManualResetEvent(false);
        static int count = 0;

        static void Main(string[] args)
        {
            MessageQueue myQueue = new MessageQueue(ConfigurationManager.AppSettings["nomeFila"]); // de posse do nome da fila ele cria uma nova instancia dessa fila
            myQueue.Formatter = new XmlMessageFormatter(new Type[] {typeof(String)}); // define o formato

            myQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(MyReceiveCompleted); // cria o evento  ReceiveCompleted que será chamado 

            myQueue.BeginReceive();

            signal.WaitOne();

            return;

        }

        private static void MyReceiveCompleted(Object source, ReceiveCompletedEventArgs asyncResult)
        {
            try
            {
                MessageQueue mq = (MessageQueue)source; //converte o objeto source para o tipo MessageQueue

                Message message = mq.EndReceive(asyncResult.AsyncResult); // obtem a mensagem

                Console.WriteLine(message.Body.ToString()); // escreve a mensagem no console

                var paciente = JsonConvert.DeserializeObject<Paciente>(message.Body.ToString()); //deserializa a mensagem de acordo com o model, nesse caso Paciente

                //instancia a classe Context para ter acesso as rotinas de banco
                using (var context = new Context()) 
                {
                    context.Paciente.Add(paciente); // insere o objeto paciente na tabela Paciente
                    context.SaveChanges(); //o objeto só é adicionado de fato após realizar a execução dessa linha, se não, não é inserido no banco
                }

                count += 1;

                if (count == 100)
                {
                    signal.Set();
                }

                mq.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                Console.WriteLine(string.Format("Erro: {0} \n Stacktrace: {1}", ex.Message, ex.StackTrace));
            }
            catch(Exception ex)
            {
                Console.WriteLine(string.Format("Erro: {0} \n Stacktrace: {1}", ex.Message, ex.StackTrace));
            }

            return;
        }
    }
}
