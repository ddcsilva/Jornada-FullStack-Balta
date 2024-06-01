using Fina.Core.Models;
using Fina.Core.Requests.Transacoes;
using Fina.Core.Responses;

namespace Fina.Core.Handlers.Interfaces;

public interface ITransacaoHandler
{
    Task<Response<Transacao?>> CriarAsync(CriarTransacaoRequest request);
    Task<Response<Transacao?>> AlterarAsync(AlterarTransacaoRequest request);
    Task<Response<Transacao?>> ExcluirAsync(ExcluirTransacaoRequest request);
    Task<Response<Transacao?>> ObterPorIdAsync(ObterTransacaoPorIdRequest request);
    Task<PaginacaoResponse<List<Transacao>?>> ObteTransacoesPorPeriodoAsync(ObterTransacoesPorPeriodoRequest request);
}
