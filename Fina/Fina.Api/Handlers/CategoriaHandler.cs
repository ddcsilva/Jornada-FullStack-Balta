using Fina.Api.Data;
using Fina.Core.Handlers.Interfaces;
using Fina.Core.Models;
using Fina.Core.Requests.Categorias;
using Fina.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handlers;

public class CategoriaHandler : ICategoriaHandler
{
    private readonly AppDbContext context;

    public CategoriaHandler(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<Response<Categoria?>> CriarAsync(CriarCategoriaRequest request)
    {
        var categoria = new Categoria
        {
            UsuarioId = request.UsuarioId,
            Nome = request.Nome,
            Descricao = request.Descricao
        };

        try
        {
            await context.Categorias.AddAsync(categoria);
            await context.SaveChangesAsync();

            return new Response<Categoria?>(categoria, 201, "Categoria criada com sucesso!");
        }
        catch (Exception)
        {
            return new Response<Categoria?>(null, 500, "Erro ao criar categoria.");
        }
    }

    public async Task<Response<Categoria?>> AlterarAsync(AlterarCategoriaRequest request)
    {
        try
        {
            var categoria = await context.Categorias.FirstOrDefaultAsync(c => c.Id == request.Id && c.UsuarioId == request.UsuarioId);

            if (categoria == null)
            {
                return new Response<Categoria?>(null, 404, "Categoria não encontrada.");
            }

            categoria.Nome = request.Nome;
            categoria.Descricao = request.Descricao;

            context.Categorias.Update(categoria);
            await context.SaveChangesAsync();

            return new Response<Categoria?>(categoria, mensagem: "Categoria alterada com sucesso!");
        }
        catch (Exception)
        {
            return new Response<Categoria?>(null, 500, "Erro ao alterar categoria.");
        }
    }

    public async Task<Response<Categoria?>> ExcluirAsync(ExcluirCategoriaRequest request)
    {
        try
        {
            var categoria = await context.Categorias.FirstOrDefaultAsync(c => c.Id == request.Id && c.UsuarioId == request.UsuarioId);

            if (categoria == null)
            {
                return new Response<Categoria?>(null, 404, "Categoria não encontrada.");
            }

            context.Categorias.Remove(categoria);
            await context.SaveChangesAsync();

            return new Response<Categoria?>(categoria, mensagem: "Categoria excluída com sucesso!");
        }
        catch (Exception)
        {
            return new Response<Categoria?>(null, 500, "Erro ao excluir categoria.");
        }
    }

    public async Task<Response<Categoria?>> ObterPorIdAsync(ObterCategoriaPorIdRequest request)
    {
        try
        {
            var categoria = await context.Categorias.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.Id && c.UsuarioId == request.UsuarioId);

            if (categoria == null)
            {
                return new Response<Categoria?>(null, 404, "Categoria não encontrada.");
            }

            return new Response<Categoria?>(categoria);
        }
        catch (Exception)
        {
            return new Response<Categoria?>(null, 500, "Erro ao obter categoria.");
        }
    }

    public async Task<PaginacaoResponse<List<Categoria>?>> ObterTodasAsync(ObterTodasCategoriasRequest request)
    {
        var query = context
            .Categorias
            .AsNoTracking()
            .Where(c => c.UsuarioId == request.UsuarioId)
            .OrderBy(c => c.Nome);

        var categorias = await query
            .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
            .Take(request.TamanhoPagina)
            .ToListAsync();

        var total = await query.CountAsync();

        return new PaginacaoResponse<List<Categoria>?>(categorias, total, request.NumeroPagina, request.TamanhoPagina);
    }
}
