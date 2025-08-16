using ClientManagerBackend.Dominio.Entities;
using ClientManagerBackend.Dominio.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientManagerBackend.Infra.Data.Mapeamentos
{
    public class ClienteMapeamento : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Nome)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(c => c.Cpf)
                .HasMaxLength(11)
                .IsRequired();

            builder
               .Property(c => c.Telefone)
               .HasMaxLength(11);

            builder
              .Property(c => c.Email)
              .HasConversion(
                    emailVO => emailVO.Address, // do VO para a coluna do banco
                    emailFromDB => new EmailVO(emailFromDB)) // do valor do banco para o VO
              .HasMaxLength(250);

            builder
              .Property(c => c.Nascimento);
        }

    }

}
