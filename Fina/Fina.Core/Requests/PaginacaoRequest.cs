namespace Fina.Core.Requests;

public class PaginacaoRequest : Request
{
    public int NumeroPagina { get; set; } = Configuracao.NumeroPaginaPadrao;
    public int TamanhoPagina { get; set; } = Configuracao.TamanhoPaginaPadrao;
}
