using Fina.Api.Common.Api;
using Fina.Core;
using Fina.Core.Handlers.Interfaces;
using Fina.Core.Models;
using Fina.Core.Requests.Transacoes;
using Fina.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.Endpoints.Transacoes;

public class ObterTransacoesPorPeriodoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Transações: Obter todas")
            .WithSummary("Recupera todas as transações")
            .WithDescription("Recupera todas as transações")
            .WithOrder(5)
            .Produces<PaginacaoResponse<List<Transacao>?>>();

    private static async Task<IResult> HandleAsync(
        ITransacaoHandler handler,
        [FromQuery] DateTime? dataInicio = null,
        [FromQuery] DateTime? dataFim = null,
        [FromQuery] int numeroPagina = Configuracao.NumeroPaginaPadrao,
        [FromQuery] int tamanhoPagina = Configuracao.TamanhoPaginaPadrao)
    {
        var request = new ObterTransacoesPorPeriodoRequest
        {
            UsuarioId = ConfiguracaoApi.UsuarioId,
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina,
            DataInicio = dataInicio,
            DataFim = dataFim
        };

        var response = await handler.ObteTransacoesPorPeriodoAsync(request);
        return response.Sucesso
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response);
    }
}