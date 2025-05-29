using ModelContextProtocol.Server;
using System.ComponentModel;

namespace server.Tools;

[McpServerToolType]
public static class EchoTool
{
    [McpServerTool, Description("Retornar a mensagem de volta")]
    public static string Echo(string message) => message;

    [McpServerTool, Description("Retornar a mensagem ao contrário de volta")]
    public static string ReverseEcho(string message) => new([.. message.Reverse()]);
}
