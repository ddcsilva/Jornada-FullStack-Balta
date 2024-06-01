using Fina.Api.Common.Api;
using Fina.Core.Handlers.Interfaces;
using Fina.Core.Models;
using Fina.Core.Requests.Categorias;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Categorias;

public class CriarCategoriaEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Categorias: Criar")
            .WithSummary("Cria uma nova categoria")
            .WithDescription("Cria uma nova categoria")
            .WithOrder(1)
            .Produces<Response<Categoria?>>();

    private static async Task<IResult> HandleAsync(ICategoriaHandler handler, CriarCategoriaRequest request)
    {
        request.UsuarioId = ConfiguracaoApi.UsuarioId;
        var response = await handler.CriarAsync(request);
        return response.Sucesso
            ? TypedResults.Created($"v1/categorias/{response.Dados?.Id}", response)
            : TypedResults.BadRequest(response);
    }
}