using Gestao.Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MioDeBaoGestao.Mapping
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome).HasMaxLength(250).HasColumnType("Varchar").IsRequired();
            builder.Property(a => a.Preco).HasColumnType("Decimal").HasPrecision(12,2).IsRequired();
            builder.Property(a => a.Quantidade).HasColumnType("int").IsRequired();


            builder.HasMany(a => a.ItensComanda).WithOne(a => a.Produto).HasForeignKey(a => a.ProdutoId);
        }
    }
}
