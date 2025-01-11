using System.Net;

namespace HttpServerLibrary.Handlers;
internal sealed class StaticFilesHandler : Handler
{
    private readonly string _staticDirectoryPath;

    public StaticFilesHandler(string staticDirectoryPath)
    {
        _staticDirectoryPath = staticDirectoryPath;
    }

    public override void HandleRequest(HttpRequestContext context)
    {
        var request = context.Request;
        bool IsGet = request.HttpMethod.Equals("GET", StringComparison.InvariantCultureIgnoreCase);
        string[] arr = request.Url.AbsolutePath.Split(".");
        bool IsFile = arr.Length == 2;

        if (IsGet && IsFile)
        {
            try
            {
                string filePath = Path.Combine(_staticDirectoryPath, request.Url.AbsolutePath.TrimStart('/'));

                if (!File.Exists(filePath))
                {
                    filePath = Path.Combine(_staticDirectoryPath, "404.html");
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                }

                byte[] responseFile = File.ReadAllBytes(filePath);
                context.Response.ContentType = GetContentType(Path.GetExtension(filePath));
                context.Response.ContentLength64 = responseFile.Length;
                context.Response.OutputStream.Write(responseFile, 0, responseFile.Length);
                context.Response.OutputStream.Close();
            }
            catch
            {
                // Обработка исключений
            }
        }
        else if (Successor != null)
        {
            Successor.HandleRequest(context);
        }
    }

    private string GetContentType(string? extension)
    {
        if (extension == null)
        {
            throw new ArgumentNullException(nameof(extension), "Extension cannot be null.");
        }

        return extension.ToLower() switch
        {
            ".html" => "text/html",
            ".css" => "text/css",
            ".js" => "application/javascript",
            ".svg" => "image/svg+xml",
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            _ => "application/octet-stream",
        };
    }
}
