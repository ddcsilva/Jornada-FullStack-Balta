using System.Text.Json.Serialization;

namespace Fina.Core.Responses;

public class Response<TDados>
{
    private int _statusCode = Configuracao.StatusCodePadrao;

    [JsonConstructor]
    public Response()
    {
        _statusCode = Configuracao.StatusCodePadrao;
    }

    public Response(TDados? dados, int statusCode = Configuracao.StatusCodePadrao, string? mensagem = null)
    {
        Dados = dados;
        _statusCode = statusCode;
        Mensagem = mensagem;
    }

    public string? Mensagem { get; set; }
    public TDados? Dados { get; set; }

    [JsonIgnore]
    public bool Sucesso => _statusCode is >= 200 and <= 299;
}
