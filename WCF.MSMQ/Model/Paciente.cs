using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WCF.MSMQ.Model
{
    [Table("Pacientes")]
    public class Paciente
    {
        [Key]
        public Guid IdPaciente { get; set; }
        public string NomePaciente { get; set; }

    }
}
