using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WCFReceptorMensagens.Model
{
    public class Context : DbContext
    {
        public Context()
            : base("name=Db")
        {
        }

        public virtual DbSet<Paciente> Paciente { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Paciente>();
        }
    }
}