using Entities.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infra.Config
{
    public class PesquisaContext : DbContext
    {
        public PesquisaContext(DbContextOptions<PesquisaContext> options) : base(options) { }

        public DbSet<Pesquisa> Empresas { get; set; }

        public DbSet<Pergunta> Pesquisas { get; set; }

        public DbSet<Opcao> Opcoes { get; set; }

        public DbSet<Resposta> Respostas { get; set; }

        public DbSet<OpcaoResposta> OpcaoResosta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);

            base.OnModelCreating(modelBuilder);
        }

    }


}

