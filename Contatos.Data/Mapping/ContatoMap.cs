using Contatos.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contatos.Data.Mapping
{
    public class ContatoMap : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder)
        {
            builder.ToTable("Contatos");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .HasColumnName("Id")
                   .IsRequired();

            builder.Property(c => c.Nome)
                   .HasColumnName("Nome")
                   .HasColumnType("VARCHAR(150)")
                   .IsRequired();

            builder.Property(c => c.DataNascimento)
                   .HasColumnName("DataNascimento")
                   .HasColumnType("DATE")
                   .IsRequired();

            builder.Property(c => c.Sexo)
                   .HasColumnName("Sexo")
                   .HasColumnType("CHAR(1)")
                   .IsRequired(false);

            builder.Property(c => c.Ativo)
                   .HasColumnName("Ativo")
                   .HasColumnType("BIT")
                   .IsRequired();
        }
    }
}
