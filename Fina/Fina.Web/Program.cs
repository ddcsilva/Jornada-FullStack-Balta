using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Fina.Web;
using MudBlazor.Services;
using Fina.Core;
using Fina.Core.Handlers.Interfaces;
using Fina.Web.Handlers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddHttpClient(ConfiguracaoWeb.NomeAplicacao, client =>
    {
        client.BaseAddress = new Uri(Configuracao.BackendUrl);
    });

builder.Services.AddTransient<ICategoriaHandler, CategoriaHandler>();

await builder.Build().RunAsync();
