using Fina.Core.Enums;

namespace Fina.Core.Models;

public class Transacao
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;

    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public DateTime? DataDePagamentoOuRecebimento { get; set; }

    public ETipoTransacao Tipo { get; set; }
    public decimal Valor { get; set; }

    public long CategoriaId { get; set; }
    public Categoria Categoria { get; set; } = null!;

    public string UsuarioId { get; set; } = string.Empty;
}
