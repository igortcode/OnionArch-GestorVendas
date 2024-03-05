using Gestao.Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MioDeBaoGestao.Mapping
{
    public class AberturaDiaMapping : IEntityTypeConfiguration<AberturaDia>
    {
        public void Configure(EntityTypeBuilder<AberturaDia> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.NmDia).HasColumnType("varchar").HasMaxLength(100).IsRequired();

            builder.HasMany(a => a.Comandas).WithOne(a => a.AberturaDia).HasForeignKey(a => a.AberturaDiaId);

            builder.Property(a => a.Faturamento).HasColumnType("Decimal").HasPrecision(12, 2).IsRequired(false);
        }
    }
}
