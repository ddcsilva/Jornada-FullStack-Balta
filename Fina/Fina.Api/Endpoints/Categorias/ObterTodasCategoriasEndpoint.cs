using Fina.Api.Common.Api;
using Fina.Core;
using Fina.Core.Handlers.Interfaces;
using Fina.Core.Models;
using Fina.Core.Requests.Categorias;
using Fina.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.Endpoints.Categorias;

public class ObterTodasCategoriasEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Categorias: Obter todos")
            .WithSummary("Recupera todas as categorias")
            .WithDescription("Recupera todas as categorias")
            .WithOrder(5)
            .Produces<PaginacaoResponse<List<Categoria>?>>();

    private static async Task<IResult> HandleAsync(
        ICategoriaHandler handler,
        [FromQuery] int numeroPagina = Configuracao.NumeroPaginaPadrao,
        [FromQuery] int tamanhoPagina = Configuracao.TamanhoPaginaPadrao)
    {
        var request = new ObterTodasCategoriasRequest
        {
            UsuarioId = ConfiguracaoApi.UsuarioId,
            NumeroPagina = numeroPagina,
            TamanhoPagina = tamanhoPagina,
        };

        var response = await handler.ObterTodasAsync(request);
        return response.Sucesso
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response);
    }
}