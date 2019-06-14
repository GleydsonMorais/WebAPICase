using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebAPICase.Models;

namespace WebAPICase.Data.Mapping
{
    public class HistoricoMap : EntityTypeConfiguration<Historico>
    {
        public HistoricoMap()
        {
            //Primary Key
            this.HasKey(h => h.idHistorico);

            //Properties
            this.Property(h => h.idHistorico)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(h => h.regiao)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(h => h.estado)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(h => h.municipio)
                .IsRequired()
                .HasMaxLength(120);

            this.Property(h => h.revenda)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(h => h.instalacaoCodigo)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(h => h.produto)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(h => h.dataColeta)
                .IsRequired();

            this.Property(h => h.valorCompra)
                .IsOptional()
                .HasPrecision(5,3);

            this.Property(h => h.valorVenda)
                .IsRequired()
                .HasPrecision(5,3);

            this.Property(h => h.undMedida)
                .IsRequired()
                .HasMaxLength(15);

            this.Property(h => h.bandeira)
                .IsRequired()
                .HasMaxLength(120);

            //Table & Column Mappings
            this.ToTable("Historico");
            this.Property(h => h.idHistorico).HasColumnName("idHistorico");
            this.Property(h => h.regiao).HasColumnName("regiao");
            this.Property(h => h.estado).HasColumnName("estado");
            this.Property(h => h.municipio).HasColumnName("municipio");
            this.Property(h => h.revenda).HasColumnName("revenda");
            this.Property(h => h.instalacaoCodigo).HasColumnName("instalacaoCodigo");
            this.Property(h => h.produto).HasColumnName("produto");
            this.Property(h => h.dataColeta).HasColumnName("dataColeta");
            this.Property(h => h.valorCompra).HasColumnName("valorCompra");
            this.Property(h => h.valorVenda).HasColumnName("valorVenda");
            this.Property(h => h.undMedida).HasColumnName("undMedida");
            this.Property(h => h.bandeira).HasColumnName("bandeira");
        }
    }
}