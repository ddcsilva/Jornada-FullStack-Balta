using Fina.Api.Common.Api;
using Fina.Core.Handlers.Interfaces;
using Fina.Core.Models;
using Fina.Core.Requests.Categorias;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Categorias;

public class ExcluirCategoriaEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Categorias: Excluir")
            .WithSummary("Exclui uma categoria")
            .WithDescription("Exclui uma categoria")
            .WithOrder(3)
            .Produces<Response<Categoria?>>();

    private static async Task<IResult> HandleAsync(ICategoriaHandler handler, long id)
    {
        var request = new ExcluirCategoriaRequest
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