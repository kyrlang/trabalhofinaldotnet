using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFTransmissorMensagens.Models
{
    public class Pacientes
    {
        [DataMember]
        public Guid IdPaciente { get; set; }
        [DataMember]
        public string NomePaciente { get; set; }

    }
}