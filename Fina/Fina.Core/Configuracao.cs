namespace Fina.Core;

public static class Configuracao
{
    public const int NumeroPaginaPadrao = 1;
    public const int TamanhoPaginaPadrao = 25;
    public const int StatusCodePadrao = 200;

    public static string BackendUrl { get; set; } = "http://localhost:5215";
    public static string FrontendUrl { get; set; } = "http://localhost:5083";
}
