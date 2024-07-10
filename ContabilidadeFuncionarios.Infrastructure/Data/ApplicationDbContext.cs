using Microsoft.EntityFrameworkCore;
using ContabilidadeFuncionarios.Domain.Entities;
using ContabilidadeFuncionarios.Domain.Enums;
using ContabilidadeFuncionarios.Domain.Models;

namespace ContabilidadeFuncionarios.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Lancamento> Lancamentos { get; set; }
        public DbSet<TaxaDesconto> TaxaDescontos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Funcionario>(entity =>
            {
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Nome).IsRequired();
                entity.Property(f => f.Sobrenome).IsRequired();
                entity.Property(f => f.Documento).IsRequired();
                entity.Property(f => f.Setor).IsRequired();
                entity.Property(f => f.SalarioBruto).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(f => f.DataAdmissao).IsRequired();
                entity.Property(f => f.PossuiPlanoSaude).IsRequired();
                entity.Property(f => f.PossuiPlanoDental).IsRequired();
                entity.Property(f => f.PossuiValeTransporte).IsRequired();
            });

            modelBuilder.Entity<Lancamento>(entity =>
            {
                entity.HasKey(l => l.Id);
                entity.Property(l => l.FuncionarioId).IsRequired();
                entity.Property(e => e.Valor).HasColumnType("decimal(18,4)").IsRequired();
                entity.Property(l => l.DataLancamento).IsRequired();
                entity.Property(l => l.Descricao).IsRequired();
            });

            modelBuilder.Entity<TaxaDesconto>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Tipo).IsRequired();
                entity.Property(e => e.Valor).IsRequired(); ;
                entity.Property(e => e.LimiteInferior);
                entity.Property(e => e.LimiteSuperior);
                entity.Property(e => e.Deducao);

            });
        }
    }
}
