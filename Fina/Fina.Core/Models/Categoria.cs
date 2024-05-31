namespace Fina.Core.Models;

public class Categoria
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Descricao { get; set; }
    public string UsuarioId { get; set; } = string.Empty;
}
