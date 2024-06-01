using Fina.Api.Common.Api;
using Fina.Core.Handlers.Interfaces;
using Fina.Core.Models;
using Fina.Core.Requests.Categorias;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Categorias;

public class ObterCategoriaPorIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Categorias: Obter por id")
            .WithSummary("Recupera uma categoria")
            .WithDescription("Recupera uma categoria")
            .WithOrder(4)
            .Produces<Response<Categoria?>>();

    private static async Task<IResult> HandleAsync(ICategoriaHandler handler, long id)
    {
        var request = new ObterCategoriaPorIdRequest
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