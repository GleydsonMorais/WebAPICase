using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebAPICase.Models;

namespace WebAPICase.Data.Mapping
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            //Primary Key
            this.HasKey(u => u.idUsuario);

            //Properties
            this.Property(u => u.idUsuario)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(u => u.nome)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(u => u.usuario)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(u => u.senha)
                .IsRequired()
                .HasMaxLength(25);

            //Table & Column Mappings
            this.ToTable("Usuario");
            this.Property(u => u.idUsuario).HasColumnName("idUsuario");
            this.Property(u => u.nome).HasColumnName("nome");
            this.Property(u => u.usuario).HasColumnName("usuario");
            this.Property(u => u.senha).HasColumnName("senha");
        }
    }
}