using Fina.Core.Handlers.Interfaces;
using Fina.Core.Requests.Categorias;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fina.Web.Pages.Categorias;

public partial class CriarCategoriaPage : ComponentBase
{
    #region Properties

    public bool EstaOcupado { get; set; } = false;
    public CriarCategoriaRequest Model { get; set; } = new();

    #endregion

    #region Services

    [Inject]
    public ICategoriaHandler Handler { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    #endregion

    #region Methods

    public async Task OnValidSubmitAsync()
    {
        EstaOcupado = true;

        try
        {
            var response = await Handler.CriarAsync(Model);
            if (response.Sucesso)
            {
                Snackbar.Add(response.Mensagem, Severity.Success);
                NavigationManager.NavigateTo("/categorias");
            }
            else
                Snackbar.Add(response.Mensagem, Severity.Error);
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

    #endregion
}