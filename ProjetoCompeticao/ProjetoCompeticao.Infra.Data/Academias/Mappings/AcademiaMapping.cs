using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoCompeticao.Domain.Academias.Entities;

namespace ProjetoCompeticao.Infra.Data.Academias.Mappings
{
    public class AcademiaMapping : IEntityTypeConfiguration<Academia>
    {
        public void Configure(EntityTypeBuilder<Academia> builder)
        {
            builder.ToTable("Academia");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .IsRequired();

            builder.Property(x => x.Nome)
                   .HasColumnName("Nome")
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(x => x.Endereco)
                  .HasColumnName("Endereco")
                  .HasMaxLength(200)
                  .IsRequired();

            builder.Property(x => x.Ativo)
                  .HasColumnName("Ativo")
                  .HasColumnType("bit")
                  .IsRequired();
        }
    }
}
