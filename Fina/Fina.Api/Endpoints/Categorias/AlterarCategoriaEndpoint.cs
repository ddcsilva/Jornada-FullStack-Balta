using Fina.Api.Common.Api;
using Fina.Core.Handlers.Interfaces;
using Fina.Core.Models;
using Fina.Core.Requests.Categorias;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Categorias;

public class AlterarCategoriaEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Categorias: Atualizar")
            .WithSummary("Atualiza uma categoria")
            .WithDescription("Atualiza uma categoria")
            .WithOrder(2)
            .Produces<Response<Categoria?>>();

    private static async Task<IResult> HandleAsync(ICategoriaHandler handler, AlterarCategoriaRequest request, long id)
    {
        request.UsuarioId = ConfiguracaoApi.UsuarioId;
        request.Id = id;

        var response = await handler.AlterarAsync(request);
        return response.Sucesso
            ? TypedResults.Ok(response)
            : TypedResults.BadRequest(response);
    }
}