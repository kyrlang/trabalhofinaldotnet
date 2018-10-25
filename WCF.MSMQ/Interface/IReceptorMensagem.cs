using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.MsmqIntegration;
using System.Text;
using System.Threading.Tasks;
using WCF.MSMQ.Model;

namespace WCF.MSMQ.Interface
{
    [ServiceContract]
    public interface IReceptorMensagem
    {

        [OperationContract(IsOneWay = true, Action = "*")]
        void BuscarMensagem(MsmqMessage<Paciente> msmqMessage);

    }
}
