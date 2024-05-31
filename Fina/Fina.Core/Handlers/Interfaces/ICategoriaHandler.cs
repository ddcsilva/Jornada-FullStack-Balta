using Fina.Core.Models;
using Fina.Core.Requests.Categorias;
using Fina.Core.Responses;

namespace Fina.Core.Handlers.Interfaces;

public interface ICategoriaHandler
{
    Task<Response<Categoria?>> CriarAsync(CriarCategoriaRequest request);
    Task<Response<Categoria?>> AlterarAsync(AlterarCategoriaRequest request);
    Task<Response<Categoria?>> ExcluirAsync(ExcluirCategoriaRequest request);
    Task<Response<Categoria?>> ObterPorIdAsync(ObterCategoriaPorIdRequest request);
    Task<PaginacaoResponse<List<Categoria>?>> ObterTodasAsync(ObterTodasCategoriasRequest request);
}
