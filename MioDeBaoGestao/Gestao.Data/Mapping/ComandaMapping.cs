using Gestao.Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MioDeBaoGestao.Mapping
{
    public class ComandaMapping : IEntityTypeConfiguration<Comanda>
    {
        public void Configure(EntityTypeBuilder<Comanda> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome).HasColumnType("varchar").HasMaxLength(255).IsRequired();

            builder.Property(a => a.ClienteId).IsRequired(false);

            builder.HasMany(a => a.Itens).WithOne(a => a.Comanda).HasForeignKey(a => a.ComandaId);

            builder.HasOne(a => a.Cliente).WithMany(a => a.Comandas).HasForeignKey(a => a.ClienteId).IsRequired(false);

            builder.HasOne(a => a.AberturaDia).WithMany(a => a.Comandas).HasForeignKey(a => a.AberturaDiaId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
