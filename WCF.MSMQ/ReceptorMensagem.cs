using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.MsmqIntegration;
using System.Text;
using System.Threading.Tasks;
using WCF.MSMQ.Interface;
using WCF.MSMQ.Model;

namespace WCF.MSMQ
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single, ReleaseServiceInstanceOnTransactionComplete = false)]
    public class ReceptorMensagem : IReceptorMensagem
    {
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void BuscarMensagem(MsmqMessage<Paciente> msmqMessage)
        {

            var orderRequest = msmqMessage.Body;
            Console.WriteLine("------------------------------------ mensagem recebida ---------------------------------------");

        }

    }
}
