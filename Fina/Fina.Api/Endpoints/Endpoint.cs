using Fina.Api.Common.Api;
using Fina.Api.Endpoints.Categorias;
using Fina.Api.Endpoints.Transacoes;

namespace Fina.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        endpoints.MapGroup("/")
            .WithTags("Health Check")
            .MapGet("/", () => new { mensagem = "OK" });

        endpoints.MapGroup("v1/categorias")
            .WithTags("Categorias")
            .MapEndpoint<CriarCategoriaEndpoint>()
            .MapEndpoint<AlterarCategoriaEndpoint>()
            .MapEndpoint<ExcluirCategoriaEndpoint>()
            .MapEndpoint<ObterCategoriaPorIdEndpoint>()
            .MapEndpoint<ObterTodasCategoriasEndpoint>();

        endpoints.MapGroup("v1/transacoes")
            .WithTags("Transacoes")
            .RequireAuthorization()
            .MapEndpoint<CriarTransacaoEndpoint>()
            .MapEndpoint<AlterarTransacaoEndpoint>()
            .MapEndpoint<ExcluirTransacaoEndpoint>()
            .MapEndpoint<ObterTransacaoPorIdEndpoint>()
            .MapEndpoint<ObterTransacoesPorPeriodoEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app) where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}