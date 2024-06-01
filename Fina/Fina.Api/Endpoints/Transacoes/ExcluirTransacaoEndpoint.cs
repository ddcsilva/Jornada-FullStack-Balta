using Azure;
using Fina.Api.Common.Api;
using Fina.Core.Handlers.Interfaces;
using Fina.Core.Models;
using Fina.Core.Requests.Transacoes;

namespace Fina.Api.Endpoints.Transacoes;

public class ExcluirTransacaoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Transações: Excluir")
            .WithSummary("Exclui uma transação")
            .WithDescription("Exclui uma transação")
            .WithOrder(3)
            .Produces<Response<Transacao?>>();

    private static async Task<IResult> HandleAsync(ITransacaoHandler handler, long id)
    {
        var request = new ExcluirTransacaoRequest
        {
            UsuarioId = ConfiguracaoApi.UsuarioId,
            Id = id
        };

        var response = await handler.ExcluirAsync(request);
        return response.Sucesso
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response);
    }
}