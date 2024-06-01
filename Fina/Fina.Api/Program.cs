using Fina.Api;
using Fina.Api.Common.Api;
using Fina.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.AdicionarConfiguracao();
builder.AdicionarContextos();
builder.AdicionarCors();
builder.AdicionarDocumentacao();
builder.AdicionarServicos();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ConfigureDevEnvironment();
}

app.UseCors(ConfiguracaoApi.CorsPolicyName);
app.MapEndpoints();

app.Run();
