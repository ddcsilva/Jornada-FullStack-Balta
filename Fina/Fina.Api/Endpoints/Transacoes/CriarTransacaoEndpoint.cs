using Azure;
using Fina.Api.Common.Api;
using Fina.Core.Handlers.Interfaces;
using Fina.Core.Models;
using Fina.Core.Requests.Transacoes;

namespace Fina.Api.Endpoints.Transacoes;

public class CriarTransacaoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Transações: Criar")
            .WithSummary("Cria uma nova transação")
            .WithDescription("Cria uma nova transação")
            .WithOrder(1)
            .Produces<Response<Transacao?>>();

    private static async Task<IResult> HandleAsync(ITransacaoHandler handler, CriarTransacaoRequest request)
    {
        request.UsuarioId = ConfiguracaoApi.UsuarioId;
        var response = await handler.CriarAsync(request);
        return response.Sucesso
            ? TypedResults.Created($"/{response.Dados?.Id}", response)
            : TypedResults.BadRequest(response.Dados);
    }
}