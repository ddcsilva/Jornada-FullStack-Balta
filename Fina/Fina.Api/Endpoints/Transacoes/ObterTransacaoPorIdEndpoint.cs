using Fina.Api.Common.Api;
using Fina.Core.Handlers.Interfaces;
using Fina.Core.Models;
using Fina.Core.Requests.Transacoes;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Transacoes;

public class ObterTransacaoPorIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Transaçõe: Obter por id")
            .WithSummary("Recupera uma transação")
            .WithDescription("Recupera uma transação")
            .WithOrder(4)
            .Produces<Response<Transacao?>>();

    private static async Task<IResult> HandleAsync(ITransacaoHandler handler, long id)
    {
        var request = new ObterTransacaoPorIdRequest
        {
            UsuarioId = ConfiguracaoApi.UsuarioId,
            Id = id
        };

        var response = await handler.ObterPorIdAsync(request);
        return response.Sucesso
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response);
    }
}