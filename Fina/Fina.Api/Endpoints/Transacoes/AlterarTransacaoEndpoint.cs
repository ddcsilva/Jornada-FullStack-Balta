using Fina.Api.Common.Api;
using Fina.Core.Handlers.Interfaces;
using Fina.Core.Models;
using Fina.Core.Requests.Transacoes;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Transacoes;

public class AlterarTransacaoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Transações: Atualizar")
            .WithSummary("Atualiza uma transação")
            .WithDescription("Atualiza uma transação")
            .WithOrder(2)
            .Produces<Response<Transacao?>>();

    private static async Task<IResult> HandleAsync(ITransacaoHandler handler, AlterarTransacaoRequest request, long id)
    {
        request.UsuarioId = ConfiguracaoApi.UsuarioId;
        request.Id = id;

        var response = await handler.AlterarAsync(request);
        return response.Sucesso
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response);
    }
}