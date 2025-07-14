using System;
using System.Net.Http;
using System.Threading.Tasks;

using System.Text.Json;

class Context7Client
{
    static async Task Main(string[] args)
    {
        if (args.Length == 0 || string.IsNullOrWhiteSpace(args[0]))
        {
            Console.WriteLine("{\"error\":\"Uso: Context7Client <nombre_libreria>\"}");
            return;
        }
        var query = args[0];
        using var http = new HttpClient();
        var searchUrl = $"https://context7.com/api/v1/search?query={Uri.EscapeDataString(query)}";
        var searchResult = await http.GetStringAsync(searchUrl);
        using var doc = JsonDocument.Parse(searchResult);
        var root = doc.RootElement;
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string fileName = $"context7_response_{timestamp}.md";
        if (root.TryGetProperty("results", out var results) && results.GetArrayLength() > 0)
        {
            var id = results[0].GetProperty("id").GetString();
            var docsUrl = $"https://context7.com/api/v1{id}";
            var docsResult = await http.GetStringAsync(docsUrl);
            // Imprimir en consola
            Console.WriteLine(docsResult);
            // Guardar en archivo Markdown con timestamp
            await System.IO.File.WriteAllTextAsync(fileName, docsResult);
        }
        else
        {
            var errorMsg = $"# Error\nNo se encontró la librería '{query}' en Context7.";
            Console.WriteLine($"{{\"error\":\"No se encontró la librería '{query}' en Context7.\"}}");
            await System.IO.File.WriteAllTextAsync(fileName, errorMsg);
        }
    }
}
