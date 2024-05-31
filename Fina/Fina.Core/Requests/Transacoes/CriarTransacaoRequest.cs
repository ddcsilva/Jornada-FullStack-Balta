using Fina.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Transacoes;

public class CriarTransacaoRequest : Request
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public ETipoTransacao Tipo { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public decimal Valor { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public long CategoriaId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    public DateTime? DataDePagamentoOuRecebimento { get; set; }
}
