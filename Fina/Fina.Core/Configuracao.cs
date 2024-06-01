namespace Fina.Core;

public static class Configuracao
{
    public const int NumeroPaginaPadrao = 1;
    public const int TamanhoPaginaPadrao = 25;
    public const int StatusCodePadrao = 200;

    public static string BackendUrl { get; set; } = string.Empty;
    public static string FrontendUrl { get; set; } = string.Empty;
}
