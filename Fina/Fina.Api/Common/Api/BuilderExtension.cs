using Fina.Api.Data;
using Fina.Api.Handlers;
using Fina.Core;
using Fina.Core.Handlers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Common.Api;

public static class BuilderExtension
{
    public static void AdicionarConfiguracao(this WebApplicationBuilder builder)
    {
        ConfiguracaoApi.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        Configuracao.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
        Configuracao.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
    }
    
    public static void AdicionarDocumentacao(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x =>
        {
            x.CustomSchemaIds(n => n.FullName);
        });
    }
    
    public static void AdicionarContextos(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddDbContext<AppDbContext>(
                x =>
                {
                    x.UseSqlServer(ConfiguracaoApi.ConnectionString);
                });
        
    }
    
    public static void AdicionarCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(
            options => options.AddPolicy(
                ConfiguracaoApi.CorsPolicyName,
                policy => policy
                    .WithOrigins([
                        Configuracao.BackendUrl,
                        Configuracao.FrontendUrl
                    ])
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            ));
    }
    
    public static void AdicionarServicos(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ICategoriaHandler, CategoriaHandler>();

        builder.Services.AddTransient<ITransacaoHandler, TransacaoHandler>();
    }
}