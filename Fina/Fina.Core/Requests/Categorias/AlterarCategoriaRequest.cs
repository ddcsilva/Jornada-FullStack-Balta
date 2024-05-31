using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Categorias;

public class AlterarCategoriaRequest : Request
{
    public long Id { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [MaxLength(80, ErrorMessage = "O campo Nome deve ter no máximo 80 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
    public string Descricao { get; set; } = string.Empty;
}
