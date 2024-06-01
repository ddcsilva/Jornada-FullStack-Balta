using Fina.Core.Handlers.Interfaces;
using Fina.Core.Models;
using Fina.Core.Requests.Categorias;
using Fina.Core.Responses;
using System.Net.Http.Json;

namespace Fina.Web.Handlers;

public class CategoriaHandler(IHttpClientFactory httpClientFactory) : ICategoriaHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(ConfiguracaoWeb.NomeAplicacao);

    public async Task<Response<Categoria?>> CriarAsync(CriarCategoriaRequest request)
    {
        var response = await _client.PostAsJsonAsync("v1/categorias", request);
        return await response.Content.ReadFromJsonAsync<Response<Categoria?>>()
            ?? new Response<Categoria?>(null, 400, "Falha ao criar categoria");
    }

    public async Task<Response<Categoria?>> AlterarAsync(AlterarCategoriaRequest request)
    {
        var response = await _client.PutAsJsonAsync($"v1/categorias/{request.Id}", request);
        return await response.Content.ReadFromJsonAsync<Response<Categoria?>>()
               ?? new Response<Categoria?>(null, 400, "Falha ao atualizar a categoria");
    }

    public async Task<Response<Categoria?>> ExcluirAsync(ExcluirCategoriaRequest request)
    {
        var response = await _client.DeleteAsync($"v1/categorias/{request.Id}");
        return await response.Content.ReadFromJsonAsync<Response<Categoria?>>()
               ?? new Response<Categoria?>(null, 400, "Falha ao excluir a categoria");
    }

    public async Task<Response<Categoria?>> ObterPorIdAsync(ObterCategoriaPorIdRequest request)
    {
        return await _client.GetFromJsonAsync<Response<Categoria?>>($"v1/categorias/{request.Id}")
           ?? new Response<Categoria?>(null, 400, "Não foi possível obter a categoria");
    }

    public async Task<PaginacaoResponse<List<Categoria>?>> ObterTodasAsync(ObterTodasCategoriasRequest request)
    {
        return await _client.GetFromJsonAsync<PaginacaoResponse<List<Categoria>?>>("v1/categorias")
           ?? new PaginacaoResponse<List<Categoria>?>(null, 400, "Não foi possível obter as categorias");
    }
}
