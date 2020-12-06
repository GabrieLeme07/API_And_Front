using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Mappings
{
    public class LoteMapping : IEntityTypeConfiguration<Lote>
    {
        public void Configure(EntityTypeBuilder<Lote> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.DescricaoLoteArquivo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

            builder.Property(e => e.DataImportacao).HasColumnType("datetime");
            builder.Property(e => e.ValorTotalImportado).HasColumnType("numeric(10, 2)");
            builder.Property(e => e.DataEntregaMenor).HasColumnType("date");

            builder.ToTable("LoteEntregas");
        }

           
    }
    
}
