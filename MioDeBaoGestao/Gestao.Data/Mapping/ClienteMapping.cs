using Gestao.Core.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MioDeBaoGestao.Mapping
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome).HasColumnType("varchar").HasMaxLength(255).IsRequired();
            builder.Property(a => a.CPF).HasColumnType("varchar").HasMaxLength(13).IsRequired();

            builder.HasMany(a => a.Comandas).WithOne(a => a.Cliente).HasForeignKey(a => a.ClienteId).IsRequired(false);


        }
    }
}
