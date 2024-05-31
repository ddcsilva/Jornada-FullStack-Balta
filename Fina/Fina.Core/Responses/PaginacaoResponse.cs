using System.Text.Json.Serialization;

namespace Fina.Core.Responses;

public class PaginacaoResponse<TDados> : Response<TDados>
{
    [JsonConstructor]
    public PaginacaoResponse(
        TDados? dados,
        int quantidadeRegistros,
        int paginaAtual = 1,
        int tamanhoPagina = Configuracao.TamanhoPaginaPadrao)
        : base(dados)
    {
        Dados = dados;
        QuantidadeRegistros = quantidadeRegistros;
        PaginaAtual = paginaAtual;
        TamanhoPagina = tamanhoPagina;
    }

    public int PaginaAtual { get; set; }
    public int TotalPaginas => (int)Math.Ceiling(QuantidadeRegistros / (double)TamanhoPagina);
    public int TamanhoPagina { get; set; } = Configuracao.TamanhoPaginaPadrao;
    public int QuantidadeRegistros { get; set; }
}