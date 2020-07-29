using GerenciadorCondominios.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominios.DAL.Mapeamentos
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {

        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(user => user.Id).ValueGeneratedOnAdd();
            builder.Property(user => user.CPF).IsRequired().HasMaxLength(30);
            builder.HasIndex(user => user.CPF).IsUnique();
            builder.Property(user => user.Foto).IsRequired();
            builder.Property(user => user.PrimeiroAcesso).IsRequired();
            builder.Property(user => user.Status).IsRequired();

            builder.HasMany(user => user.PropietariosApartamentos).WithOne(user => user.Proprietario);
            builder.HasMany(user => user.MoradoresApartamentos).WithOne(user => user.Morador);
            builder.HasMany(user => user.Veiculos).WithOne(user => user.Usuario);
            builder.HasMany(user => user.Eventos).WithOne(user => user.Usuario);
            builder.HasMany(user => user.Pagamentos).WithOne(user => user.Usuario);
            builder.HasMany(user => user.Servicos).WithOne(user => user.Usuario);

            builder.ToTable("Usuarios");
        }
    }
}
