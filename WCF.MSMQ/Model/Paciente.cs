using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCF.MSMQ.Model
{
    [DataContract]
    public class Paciente
    {

        [DataMember]
        public Guid IdPaciente { get; set; }
        [DataMember]
        public string NomePaciente { get; set; }

    }
}
