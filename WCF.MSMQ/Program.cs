using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF.MSMQ
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var host = new ServiceHost(typeof(ReceptorMensagem)))
            {
                host.Faulted += Faulted;
                host.Open();

                Console.WriteLine("Serviço iniciado ...");

                //Se apertar qualquer tecla vai sair do console
                Console.ReadKey();

                if (host != null)
                {
                    if (host.State == CommunicationState.Faulted)
                    {
                        host.Abort();
                    }
                    host.Close();
                }
            }

        }

        private static void Faulted(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
