using Fina.Core.Handlers.Interfaces;
using Fina.Core.Models;
using Fina.Core.Requests.Categorias;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fina.Web.Pages.Categorias;

public partial class EditarCategoriaPage : ComponentBase
{
    [Parameter]
    public long Id { get; set; }

    public bool EstaOcupado { get; set; } = false;
    public Categoria Model { get; set; } = new();

    [Inject]
    public ICategoriaHandler Handler { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        EstaOcupado = true;
        try
        {
            var request = new ObterCategoriaPorIdRequest { Id = Id };
            var result = await Handler.ObterPorIdAsync(request);
            if (result.Sucesso)
            {
                Model = result.Dados;
            }
            else
            {
                Snackbar.Add(result.Mensagem, Severity.Error);
                NavigationManager.NavigateTo("/categorias");
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
            NavigationManager.NavigateTo("/categorias");
        }
        finally
        {
            EstaOcupado = false;
        }
    }

    public async Task OnValidSubmitAsync()
    {
        EstaOcupado = true;
        try
        {
            var updateRequest = new AlterarCategoriaRequest
            {
                Id = Model.Id,
                Nome = Model.Nome,
                Descricao = Model.Descricao
            };
            var response = await Handler.AlterarAsync(updateRequest);
            if (response.Sucesso)
            {
                Snackbar.Add(response.Mensagem, Severity.Success);
                NavigationManager.NavigateTo("/categorias");
            }
            else
            {
                Snackbar.Add(response.Mensagem, Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            EstaOcupado = false;
        }
    }
}
