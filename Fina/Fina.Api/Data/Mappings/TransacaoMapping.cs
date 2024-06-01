using Fina.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fina.Api.Data.Mappings;

public class TransacaoMapping : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.ToTable("Transacao");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Nome)
            .IsRequired(true)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.Property(t => t.Tipo)
            .IsRequired(true)
            .HasColumnType("SMALLINT");

        builder.Property(t => t.Valor)
            .IsRequired(true)
            .HasColumnType("MONEY");

        builder.Property(t => t.DataCriacao)
            .IsRequired(true);

        builder.Property(t => t.DataDePagamentoOuRecebimento)
            .IsRequired(false);

        builder.Property(t => t.UsuarioId)
            .IsRequired(true)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
    }
}
