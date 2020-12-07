using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Context
{
    public partial class MeuDbContext : DbContext
    {
        public MeuDbContext(DbContextOptions<MeuDbContext> options) : base(options) { }

        public DbSet<ExcelData> ExcelDatas { get; set; }
        public DbSet<Lote> LoteEntregas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Registra todas as classes
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);
        }

    }
}
