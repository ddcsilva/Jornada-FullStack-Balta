using Fina.Api.Data;
using Fina.Core.Common;
using Fina.Core.Enums;
using Fina.Core.Handlers.Interfaces;
using Fina.Core.Models;
using Fina.Core.Requests.Transacoes;
using Fina.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handlers;

public class TransacaoHandler : ITransacaoHandler
{
    private readonly AppDbContext context;

    public TransacaoHandler(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<Response<Transacao?>> CriarAsync(CriarTransacaoRequest request)
    {
        if (request is { Tipo: ETipoTransacao.Saida, Valor: >= 0 })
        {
            request.Valor *= -1;
        }

        try
        {
            var transacao = new Transacao
            {
                UsuarioId = request.UsuarioId,
                CategoriaId = request.CategoriaId,
                DataCriacao = DateTime.Now,
                Valor = request.Valor,
                DataDePagamentoOuRecebimento = request.DataDePagamentoOuRecebimento,
                Nome = request.Nome,
                Tipo = request.Tipo
            };

            await context.Transacoes.AddAsync(transacao);
            await context.SaveChangesAsync();

            return new Response<Transacao?>(transacao, 201, "Transação criada com sucesso!");
        }
        catch
        {
            return new Response<Transacao?>(null, 500, "Não foi possível criar sua transação");
        }
    }

    public async Task<Response<Transacao?>> AlterarAsync(AlterarTransacaoRequest request)
    {
        if (request is { Tipo: ETipoTransacao.Saida, Valor: >= 0 })
        {
            request.Valor *= -1;
        }

        try
        {
            var transacao = await context
                .Transacoes
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UsuarioId == request.UsuarioId);

            if (transacao == null)
            {
                return new Response<Transacao?>(null, 404, "Transação não encontrada");
            }

            transacao.CategoriaId = request.CategoriaId;
            transacao.Valor = request.Valor;
            transacao.Nome = request.Nome;
            transacao.Tipo = request.Tipo;
            transacao.DataDePagamentoOuRecebimento = request.DataDePagamentoOuRecebimento;

            context.Transacoes.Update(transacao);
            await context.SaveChangesAsync();

            return new Response<Transacao?>(transacao);
        }
        catch
        {
            return new Response<Transacao?>(null, 500, "Não foi possível recuperar sua transação");
        }
    }

    public async Task<Response<Transacao?>> ExcluirAsync(ExcluirTransacaoRequest request)
    {
        try
        {
            var transacao = await context
                .Transacoes
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UsuarioId == request.UsuarioId);

            if (transacao == null)
            {
                return new Response<Transacao?>(null, 404, "Transação não encontrada");
            }

            context.Transacoes.Remove(transacao);
            await context.SaveChangesAsync();

            return new Response<Transacao?>(transacao);
        }
        catch
        {
            return new Response<Transacao?>(null, 500, "Não foi possível recuperar sua transação");
        }
    }

    public async Task<Response<Transacao?>> ObterPorIdAsync(ObterTransacaoPorIdRequest request)
    {
        try
        {
            var transacao = await context
                .Transacoes
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UsuarioId == request.UsuarioId);

            if (transacao == null)
            {
                return new Response<Transacao?>(null, 404, "Transação não encontrada");
            }

            return new Response<Transacao?>(transacao);
        }
        catch
        {
            return new Response<Transacao?>(null, 500, "Não foi possível recuperar sua transação");
        }
    }

    public async Task<PaginacaoResponse<List<Transacao>?>> ObteTransacoesPorPeriodoAsync(ObterTransacoesPorPeriodoRequest request)
    {
        try
        {
            request.DataInicio ??= DateTime.Now.ObterPrimeiroDia();
            request.DataFim ??= DateTime.Now.ObterSegundoDia();
        }
        catch
        {
            return new PaginacaoResponse<List<Transacao>?>(null, 500, "Não foi possível determinar a data de início ou término");
        }

        try
        {
            var query = context
                .Transacoes
                .AsNoTracking()
                .Where(x =>
                    x.DataDePagamentoOuRecebimento >= request.DataInicio &&
                    x.DataDePagamentoOuRecebimento <= request.DataFim &&
                    x.UsuarioId == request.UsuarioId)
                .OrderBy(x => x.DataDePagamentoOuRecebimento);

            var transacoes = await query
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            var total = await query.CountAsync();

            return new PaginacaoResponse<List<Transacao>?>(
                transacoes,
                total,
                request.NumeroPagina,
                request.TamanhoPagina);
        }
        catch
        {
            return new PaginacaoResponse<List<Transacao>?>(null, 500, "Não foi possível obter as transações");
        }
    }
}
