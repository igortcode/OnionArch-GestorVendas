using Gestao.Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MioDeBaoGestao.Mapping
{
    public class ItensComandaMapping : IEntityTypeConfiguration<ItemComanda>
    {
        public void Configure(EntityTypeBuilder<ItemComanda> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome).HasMaxLength(250).HasColumnType("Varchar").IsRequired();
            builder.Property(a => a.Preco).HasColumnType("Decimal").HasPrecision(12, 2).IsRequired();
            builder.Property(a => a.Quantidade).HasColumnType("int").IsRequired();

            builder.HasOne(a => a.Comanda).WithMany(a => a.Itens).HasForeignKey(a => a.ComandaId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(a => a.Produto).WithMany(a => a.ItensComanda).HasForeignKey(a => a.ProdutoId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
