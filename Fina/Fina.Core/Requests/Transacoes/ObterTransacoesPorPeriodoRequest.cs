namespace Fina.Core.Requests.Transacoes;

public class ObterTransacoesPorPeriodoRequest : PaginacaoRequest
{
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
}
