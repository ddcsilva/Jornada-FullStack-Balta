using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Categorias;

public class CriarCategoriaRequest : Request
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [MaxLength(80, ErrorMessage = "O campo Nome deve ter no máximo 80 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
    public string Descricao { get; set; } = string.Empty;
}
