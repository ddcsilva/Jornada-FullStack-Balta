using Fina.Core.Handlers.Interfaces;
using Fina.Core.Models;
using Fina.Core.Requests.Categorias;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fina.Web.Pages.Categorias;

public partial class ObterTodasCategoriasPage : ComponentBase
{
    public bool EstaOcupado { get; set; } = false;
    public List<Categoria> Categorias { get; set; } = new();

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IDialogService Dialog { get; set; } = null!;

    [Inject]
    public ICategoriaHandler Handler { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        EstaOcupado = true;
        try
        {
            var request = new ObterTodasCategoriasRequest();
            var result = await Handler.ObterTodasAsync(request);
            if (result.Sucesso)
                Categorias = result.Dados ?? new List<Categoria>();
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

    public void OnEditButtonClickedAsync(long id)
    {
        NavigationManager.NavigateTo($"/categorias/editar/{id}");
    }

    public async Task OnDeleteButtonClickedAsync(long id, string nome)
    {
        var result = await Dialog.ShowMessageBox(
            "ATENÇÃO",
            $"Ao prosseguir a categoria {nome} será removida. Deseja continuar?",
            yesText: "Excluir",
            cancelText: "Cancelar");

        if (result is true)
            await OnDeleteAsync(id, nome);

        StateHasChanged();
    }

    public async Task OnDeleteAsync(long id, string titulo)
    {
        try
        {
            var request = new ExcluirCategoriaRequest
            {
                Id = id
            };
            await Handler.ExcluirAsync(request);
            Categorias.RemoveAll(x => x.Id == id);
            Snackbar.Add($"Categoria {titulo} removida", Severity.Info);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
}
