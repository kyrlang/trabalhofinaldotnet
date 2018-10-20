using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WCFReceptorMensagens.Model
{
    [Table("Pacientes")]
    public class Paciente
    {
        [Key]
        public Guid IdPaciente { get; set; }
        public string NomePaciente { get; set; }
    }
}