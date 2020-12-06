using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Mappings
{
    public class ExcelDataMapping : IEntityTypeConfiguration<ExcelData>
    {
        public void Configure(EntityTypeBuilder<ExcelData> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.DataEntrega)
            .HasColumnType("date")
            .IsRequired();

            builder.Property(e => e.DescricaoProduto)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ValorUnitario).HasColumnType("numeric(10, 2)");

            builder.HasOne(d => d.Lote)
                .WithMany(p => p.ExcelDatas)
                .HasForeignKey(d => d.IdLoteEntrega)
                .HasPrincipalKey(p => p.Id);

            builder.ToTable("ExcelDatas");
        }


    }
    
}
