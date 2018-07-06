namespace estacionamento.Models.model1
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<controle_estacionamento> controle_estacionamento { get; set; }
        public virtual DbSet<vigencia_valor> vigencia_valor { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<controle_estacionamento>()
                .Property(e => e.placa)
                .IsUnicode(false);

            modelBuilder.Entity<controle_estacionamento>()
                .Property(e => e.horario_chegada)
                .IsUnicode(false);

            modelBuilder.Entity<controle_estacionamento>()
                .Property(e => e.horario_saida)
                .IsUnicode(false);

            modelBuilder.Entity<controle_estacionamento>()
                .Property(e => e.duracao)
                .IsUnicode(false);

            modelBuilder.Entity<vigencia_valor>()
                .Property(e => e.dt_inicio)
                .IsUnicode(false);

            modelBuilder.Entity<vigencia_valor>()
                .Property(e => e.dt_fim)
                .IsUnicode(false);
        }
    }
}
