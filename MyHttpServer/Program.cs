using HttpServerLibrary.Configurations;
using MyHttpServer.Models;
using MyORMLibrary;
using System.Data.SqlClient;
using TemplateEngine;
namespace HttpServerLibrary;

internal class Program
{
    static async Task Main()
    {
        // Загружаем конфигурацию через Singleton
        var config = AppConfig.Instance;

        // Проверяем наличие статического каталога
        if (!Directory.Exists(config.StaticDirectoryPath))
        {
            Console.WriteLine($"Статическая директория \"{config.StaticDirectoryPath}\" не найдена.");
            Directory.CreateDirectory(config.StaticDirectoryPath);
            Console.WriteLine($"Создана новая директория: \"{config.StaticDirectoryPath}\"");
        }
        try
        {
            var server = new HttpServer(new[] { $"http://{config.Domain}:{config.Port}/" }, config.StaticDirectoryPath);

            Console.WriteLine($"Сервер запущен на http://{config.Domain}:{config.Port}/");
            await server.StartAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при запуске сервера: {ex.Message}");
        }
    }
}