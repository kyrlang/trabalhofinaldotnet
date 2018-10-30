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
            MessageQueue myQueue = new MessageQueue(ConfigurationManager.AppSettings["nomeFila"]);
            myQueue.Formatter = new XmlMessageFormatter(new Type[]
                {typeof(String)});

            myQueue.ReceiveCompleted +=
                new ReceiveCompletedEventHandler(MyReceiveCompleted);

            myQueue.BeginReceive();

            signal.WaitOne();

            return;

        }

        private static void MyReceiveCompleted(Object source,
            ReceiveCompletedEventArgs asyncResult)
        {
            try
            {
                MessageQueue mq = (MessageQueue)source;

                Message message = mq.EndReceive(asyncResult.AsyncResult);

                Console.WriteLine(message.Body.ToString());

                var paciente = JsonConvert.DeserializeObject<Paciente>(message.Body.ToString());

                using (var context = new Context())
                {
                    context.Paciente.Add(paciente);
                    context.SaveChanges();
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
