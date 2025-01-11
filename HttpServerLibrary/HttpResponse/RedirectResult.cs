﻿namespace HttpServerLibrary.HttpResponse
{
    public class RedirectResult : IHttpResponseResult
    {
        private readonly string _location;
        public RedirectResult(string location)
        {
            _location = location;
        }

        public void Execute(HttpRequestContext context)
        {
            var response = context.Response;
            response.StatusCode = 302;
            response.Headers.Add("Location", _location);

            // Добавляем минимальное тело ответа
            using (var writer = new StreamWriter(response.OutputStream))
            {
                writer.Write($"<html><body>Redirecting to <a href=\"{_location}\">{_location}</a></body></html>");
            }
            response.Close(); // Завершение
        }
    }
}
