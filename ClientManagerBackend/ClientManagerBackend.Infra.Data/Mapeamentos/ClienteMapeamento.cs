using ClientManagerBackend.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

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
              .HasMaxLength(250);

            builder
              .Property(c => c.Nascimento);
        }

    }

}
