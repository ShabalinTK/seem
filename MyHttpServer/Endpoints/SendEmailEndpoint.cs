using ClassLibrary1;
using HttpServerLibrary;
using HttpServerLibrary.Attributes;
using HttpServerLibrary.HttpResponse;
using MyHttpServer.Models;
using MyHttpServer.Services;
using MyORMLibrary;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Web;
using TemplateEngine;

namespace MyHttpServer.Endpoints
{
    internal class SendEmailEndpoint : EndpointBase
    {
        static public List<Movie> GetMovies()
        {
            using var connection = new SqlConnection(@"Data Source=localhost;Initial Catalog=User;User ID=sa;Password=P@ssw0rd");

            // Изменяем запрос, чтобы получить информацию о режиссере в одном запросе
            var query = @"
        SELECT m.Id, m.Title, m.TitleEng, m.ReleaseYear, m.Country, m.Genre, m.Description, m.Player, m.ImagePath, m.Actors, d.Name AS DirectorName
        FROM Movies m
        LEFT JOIN Directors d ON m.DirectorId = d.Id";

            var movies = new List<Movie>();

            using var command = new SqlCommand(query, connection);
            connection.Open();

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var movie = new Movie
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    TitleEng = reader.GetString(2),
                    ReleaseYear = reader.GetString(3),
                    Country = reader.GetString(4),
                    Genre = reader.GetString(5),
                    Description = reader.GetString(6),
                    Player = reader.GetString(7),
                    ImagePath = reader.GetString(8),
                    Actors = reader.GetString(9),
                    Director = new Director // Теперь загружаем информацию о режиссере
                    {
                        Name = reader.IsDBNull(10) ? "Unknown" : reader.GetString(10)
                    }
                };

                movies.Add(movie);
            }

            return movies;
        }
        [Get("movies")]
        public IHttpResponseResult GetMoviesPage()
        {
            // Определяем путь к HTML-шаблону
            string templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "public", "web", "main", "index.html");

            if (!File.Exists(templatePath))
            {
                return Html("<h1>404 - File Not Found</h1>");
            }

            // Получаем токен из куки
            var tokenCookie = Context.Request.Cookies["session-token"];
            string username = "Вход"; // Значение по умолчанию

            if (tokenCookie != null)
            {
                string userId = SessionStorage.GetUserId(tokenCookie.Value);
                if (!string.IsNullOrEmpty(userId))
                {
                    string connectionString = @"Data Source=localhost;Initial Catalog=User;User ID=sa;Password=P@ssw0rd";

                    using var sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();

                    string query = "SELECT Login FROM Person WHERE Id = @Id";

                    using var command = new SqlCommand(query, sqlConnection);
                    command.Parameters.AddWithValue("@Id", userId);

                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        username = result.ToString();
                    }
                }
            }

            // Читаем HTML-шаблон
            string content = File.ReadAllText(templatePath);

            // Заменяем плейсхолдер {{login_name}} на имя пользователя
            content = content.Replace("{{login_name}}", username);

            // Получаем список фильмов
            var movies = GetMovies();

            // Формируем HTML для списка фильмов
            var moviesHtml = new StringBuilder();
            foreach (var movie in movies)
            {
                moviesHtml.Append($@"
<div class='poster grid-item has-overlay'> 
    <div class='poster__img img-responsive img-responsive--portrait img-fit-cover anim'> 
        <img alt='{movie.Title}' loading='lazy' src='{movie.ImagePath}' /> 
    </div> 
    <div class='poster__desc'> 
        <h3 class='poster__title'> 
            <a href='/movie?id={movie.Id}'>
                <span class='ws-nowrap'>{movie.Title}</span>
            </a> 
        </h3> 
        <ul class='poster__subtitle ws-nowrap'> 
            <li>{movie.Genre}</li> 
        </ul> 
    </div> 
</div>");
            }

            // Используем HTML-шаблонный движок для вставки контента
            var engine = new HtmlTemplateEngine();
            var finalHtml = engine.RenderToId(content, "dle-content", moviesHtml.ToString());

            // Возвращаем итоговый HTML как ответ
            return Html(finalHtml);
        }

        [Post("movies")]
        public IHttpResponseResult Login(string login_name, string login_password)
        {
            string connectionString = @"Data Source=localhost;Initial Catalog=User;User ID=sa;Password=P@ssw0rd";

            using var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = "SELECT Id, Login FROM Person WHERE Login = @Login AND Password = @Password";

            using (var command = new SqlCommand(query, sqlConnection))
            {
                command.Parameters.AddWithValue("@Login", login_name);
                command.Parameters.AddWithValue("@Password", login_password);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        string userId = reader["Id"].ToString();
                        var token = Guid.NewGuid().ToString();

                        Context.Response.Cookies.Add(new Cookie("session-token", token)
                        {
                            Path = "/"
                        });

                        SessionStorage.SaveSession(token, userId);

                        return Redirect("/movies");
                    }
                }
            }
            return Redirect("/movies");
        }
        [Get("movie")]
        public IHttpResponseResult GetMoviePage(int id)
        {
            var movieService = new MovieService();
            var movie = movieService.GetMovieWithDetails(id);

            if (movie == null)
            {
                return Html("<h1>Фильм не найден</h1>");
            }

            // Получаем токен из куки
            var tokenCookie = Context.Request.Cookies["session-token"];
            string username = "Вход"; // Значение по умолчанию, если нет токена
            string userId = null;

            if (tokenCookie != null)
            {
                userId = SessionStorage.GetUserId(tokenCookie.Value);
                if (userId != null)
                {
                    using var sqlConnection = new SqlConnection(@"Data Source=localhost;Initial Catalog=User;User ID=sa;Password=P@ssw0rd");
                    sqlConnection.Open();

                    string query = "SELECT Login FROM Person WHERE Id = @Id";
                    using (var command = new SqlCommand(query, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@Id", userId);

                        var result = command.ExecuteScalar();
                        if (result != null)
                        {
                            username = result.ToString();
                        }
                    }
                }
            }

            // Загружаем HTML-шаблон
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "public", "web", "venom", "index.html");
            string template = File.ReadAllText(filePath);

            // Заменяем плейсхолдеры данными фильма
            template = template.Replace("{{movie.Title}}", movie.Title)
                               .Replace("{{movie.TitleEng}}", movie.TitleEng)
                               .Replace("{{movie.ReleaseYear}}", movie.ReleaseYear)
                               .Replace("{{movie.Country}}", movie.Country)
                               .Replace("{{movie.Genre}}", movie.Genre)
                               .Replace("{{movie.ImagePath}}", movie.ImagePath)
                               .Replace("{{movie.Director.Name}}", movie.Director.Name)
                               .Replace("{{movie.Actors}}", movie.Actors)
                               .Replace("{{movie.Description}}", movie.Description)
                               .Replace("{{movie.Player}}", movie.Player)
                               .Replace("{{login_name}}", username)
                               .Replace("{{movie.Likes}}", movie.LikesCount.ToString())
                               .Replace("{{movie.Dislikes}}", movie.DislikesCount.ToString());

            // Рендерим HTML
            return Html(template);
        }

        [Post("movie")]
        public IHttpResponseResult Movie(string login_name, string login_password)
        {
            string connectionString = @"Data Source=localhost;Initial Catalog=User;User ID=sa;Password=P@ssw0rd";

            using var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = "SELECT Id, Login FROM Person WHERE Login = @Login AND Password = @Password";

            using (var command = new SqlCommand(query, sqlConnection))
            {
                command.Parameters.AddWithValue("@Login", login_name);
                command.Parameters.AddWithValue("@Password", login_password);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        string userId = reader["Id"].ToString();
                        var token = Guid.NewGuid().ToString();

                        Context.Response.Cookies.Add(new Cookie("session-token", token)
                        {
                            Path = "/"
                        });

                        SessionStorage.SaveSession(token, userId);

                        return Redirect("/movies");
                    }
                }
            }
            return Redirect("/movies");
        }
        [Post("movie/stat")]
        public IHttpResponseResult Stat(string comments, string name, string mail, int movieId)
        {
            // Проверяем переданные параметры
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(comments))
            {
                return Json(new { success = false, message = "Имя и комментарий обязательны!" });
            }

            // Строка соединения с базой данных
            string connectionString = @"Data Source=localhost;Initial Catalog=User;User ID=sa;Password=P@ssw0rd";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // SQL-запрос для вставки данных в таблицу Comments
                    string query = "INSERT INTO Comments (UserName, Email, CommentText, MovieId) VALUES (@UserName, @Email, @CommentText, @MovieId)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        // Добавляем параметры для защиты от SQL инъекций
                        command.Parameters.AddWithValue("@UserName", name);
                        command.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(mail) ? (object)DBNull.Value : mail);
                        command.Parameters.AddWithValue("@CommentText", comments);
                        command.Parameters.AddWithValue("@MovieId", movieId); // Используем переданный movieId

                        // Выполняем запрос
                        command.ExecuteNonQuery();
                    }
                }

                // Возвращаем успешный ответ
                return Json(new { success = true, message = "Комментарий успешно добавлен!" });
            }
            catch (Exception ex)
            {
                // В случае ошибки возвращаем сообщение об ошибке
                return Json(new { success = false, message = "Ошибка при добавлении комментария: " + ex.Message });
            }
        }
        [Post("movie/rate")]
        public IHttpResponseResult RateMovie()
        {
            var tokenCookie = Context.Request.Cookies["session-token"];
            string userId = null;

            if (tokenCookie != null)
            {
                userId = SessionStorage.GetUserId(tokenCookie.Value);
            }

            if (userId == null)
            {
                return Json(new { success = false, message = "Вы не авторизованы." });
            }

            string body;
            using (var reader = new StreamReader(Context.Request.InputStream))
            {
                body = reader.ReadToEnd();
            }

            MovieStats rateRequest;
            try
            {
                rateRequest = JsonSerializer.Deserialize<MovieStats>(body);
            }
            catch (JsonException)
            {
                return Json(new { success = false, message = "Ошибка в формате данных." });
            }

            string connectionString = @"Data Source=localhost;Initial Catalog=User;User ID=sa;Password=P@ssw0rd";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                try
                {
                    string checkQuery = "SELECT COUNT(*), SUM(CASE WHEN IsLike = 1 THEN 1 ELSE 0 END) as LikesCount FROM MovieStats WHERE MovieId = @MovieId AND UserId = @UserId";
                    using (var command = new SqlCommand(checkQuery, connection))
                    {
                        command.Parameters.Add("@MovieId", SqlDbType.Int).Value = rateRequest.MovieId;
                        command.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = userId;

                        using (var reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                return Json(new { success = false, message = "Ошибка при обработке данных." });
                            }

                            int existingCount = reader.GetInt32(0);
                            int likesCount = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);

                            if (existingCount > 0)
                            {
                                string updateQuery = "UPDATE MovieStats SET IsLike = @IsLike WHERE MovieId = @MovieId AND UserId = @UserId";
                                using (var updateCommand = new SqlCommand(updateQuery, connection))
                                {
                                    updateCommand.Parameters.Add("@IsLike", SqlDbType.Bit).Value = rateRequest.IsLike;

                                    updateCommand.Parameters.Add("@MovieId", SqlDbType.Int).Value = rateRequest.MovieId;
                                    updateCommand.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = userId;

                                    updateCommand.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                string insertQuery = "INSERT INTO MovieStats (MovieId, UserId, IsLike) VALUES (@MovieId, @UserId, @IsLike)";
                                using (var insertCommand = new SqlCommand(insertQuery, connection))
                                {
                                    insertCommand.Parameters.Add("@MovieId", SqlDbType.Int).Value = rateRequest.MovieId;
                                    insertCommand.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = userId;
                                    insertCommand.Parameters.Add("@IsLike", SqlDbType.Bit).Value = rateRequest.IsLike;

                                    insertCommand.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    string countQuery = "SELECT SUM(CASE WHEN IsLike = 1 THEN 1 ELSE 0 END) as LikesCount, SUM(CASE WHEN IsLike = 0 THEN 1 ELSE 0 END) as DislikesCount FROM MovieStats WHERE MovieId = @MovieId";

                    using (var countCommand = new SqlCommand(countQuery, connection))
                    {
                        countCommand.Parameters.Add("@MovieId", SqlDbType.Int).Value = rateRequest.MovieId;

                        using (var countReader = countCommand.ExecuteReader())
                        {
                            if (countReader.Read())
                            {
                                int newLikesCount = countReader.IsDBNull(0) ? 0 : countReader.GetInt32(0);
                                int newDislikesCount = countReader.IsDBNull(1) ? 0 : countReader.GetInt32(1);

                                return Json(new { success = true, likesCount = newLikesCount, dislikesCount = newDislikesCount });
                            }
                            else
                            {
                                return Json(new { success = false, message = "Ошибка при получении рейтинга." });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Ошибка при взаимодействии с базой данных.", details = ex.Message });
                }
            }
        }

        [Get("register")]
        public IHttpResponseResult GetRegisterPage()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "public", "web", "reg", "index.html");

            if (!File.Exists(filePath))
            {
                return Html("<h1>404 - File Not Found</h1>");
            }

            // Получаем токен из куки
            var tokenCookie = Context.Request.Cookies["session-token"];
            string username = "Вход";

            if (tokenCookie != null)
            {
                string userId = SessionStorage.GetUserId(tokenCookie.Value);
                if (userId != null)
                {
                    string connectionString = @"Data Source=localhost;Initial Catalog=User;User ID=sa;Password=P@ssw0rd";

                    using var sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();

                    string query = "SELECT Login FROM Person WHERE Id = @Id";

                    using (var command = new SqlCommand(query, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@Id", userId);

                        var result = command.ExecuteScalar();
                        if (result != null)
                        {
                            username = result.ToString();
                        }
                    }
                }
            }

            // Читаем HTML и заменяем плейсхолдер {{username}}
            string content = File.ReadAllText(filePath);
            content = content.Replace("{{login_name}}", username);

            return Html(content);
        }

        [Post("register")]
        public IHttpResponseResult Register(string username, string password, string email)
        {
            // Проверяем входные данные
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                return Redirect("/register");
            }

            username = username.Trim();
            password = password.Trim();
            email = email.Trim();

            string connectionString = @"Data Source=localhost;Initial Catalog=User;User ID=sa;Password=P@ssw0rd";

            using var sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            // Проверяем, существует ли пользователь с таким логином или email
            string query = "SELECT COUNT(*) FROM Person WHERE Login = @Login OR Email = @Email";

            using (var command = new SqlCommand(query, sqlConnection))
            {
                command.Parameters.AddWithValue("@Login", username);
                command.Parameters.AddWithValue("@Email", email);

                int userCount = (int)command.ExecuteScalar();

                if (userCount > 0)
                {
                    // Если логин или email уже существует, перенаправляем на страницу регистрации
                    return Redirect("/register");
                }
            }

            // Если пользователя нет, добавляем его в базу данных
            string insertUserQuery = "INSERT INTO Person (Login, Password, Email) VALUES (@Login, @Password, @Email)";

            using (var insertCommand = new SqlCommand(insertUserQuery, sqlConnection))
            {
                insertCommand.Parameters.AddWithValue("@Login", username);
                insertCommand.Parameters.AddWithValue("@Password", password);  // Для безопасности используйте хеширование пароля
                insertCommand.Parameters.AddWithValue("@Email", email);

                insertCommand.ExecuteNonQuery();
            }

            // После успешной регистрации перенаправляем на страницу логина
            return Redirect("/movies");
        }
        [Get("admin")]
        public IHttpResponseResult GetAdminPage()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "public", "web", "admin", "index.html");

            if (!File.Exists(filePath))
            {
                return Html("<h1>404 - File Not Found</h1>");
            }

            var movies = GetMovies();
            var moviesHtml = "";

            foreach (var movie in movies)
            {

                string directorName = movie.Director != null ? movie.Director.Name : "Unknown"; // Здесь мы выводим имя режиссера
                moviesHtml += $@"
       <div class='movie-item'>
           <div>
               <strong>{movie.Title} | {movie.TitleEng}</strong><br>
               <em>{movie.Genre}</em><br>
               <strong>{movie.ReleaseYear}</strong>
           </div>
           <div>
               <p>Режиссер: {directorName}</p>
               <p>Актеры: {movie.Actors}</p>
               <p><a href='{movie.Player}' target='_blank'>Перейти к фильму</a></p>
           </div>
           <div>
               <form action='/admin/delete-movie' method='POST'>
                   <input type='hidden' name='id' value='{movie.Id}'>
                   <button type='submit'>Удалить</button>
               </form>
           </div>
       </div>";
            }

            // Читаем шаблон и заменяем плейсхолдер {{moviesHtml}}
            string content = File.ReadAllText(filePath);
            content = content.Replace("{{moviesHtml}}", moviesHtml);

            return Html(content);
        }


        [Post("admin/delete-movie")]
        public IHttpResponseResult DeleteFilm(int id)
        {
            try
            {
                using (var sqlConnection = new SqlConnection("Data Source=localhost;Initial Catalog=User;User ID=sa;Password=P@ssw0rd"))
                {
                    sqlConnection.Open();

                    string query = "DELETE FROM Movies WHERE Id = @id"; // Исправлено имя таблицы с Movie на Movies 

                    using (var command = new SqlCommand(query, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0) // Фильм не найден
                        {
                            return Json(new { success = false, message = "Фильм не найден" });
                        }
                    }
                }

                // Перенаправляем на страницу управления
                return Redirect("/admin"); // Возврат на страницу с фильмами после удаления
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении фильма: {ex.Message}");
                return Json(new { success = false, message = "Не удалось удалить фильм" });
            }
        }
        [Post("admin/update")]
        public IHttpResponseResult UpdateFilm(int id, string title, string titleEng, string releaseYear, string country, string genre, string description, string player, string imagePath, string actors)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(@"Data Source=localhost;Initial Catalog=User;User ID=sa;Password=P@ssw0rd"))
                {
                    sqlConnection.Open();

                    // Обновляем информацию о фильме в таблице Movies
                    string query = @"
                    UPDATE Movies 
                    SET Title = @Title, TitleEng = @TitleEng, ReleaseYear = @ReleaseYear, Country = @Country,
                        Genre = @Genre, Description = @Description, Player = @Player, ImagePath = @ImagePath, 
                        Actors = @Actors
                    WHERE Id = @Id";

                    using (var command = new SqlCommand(query, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Title", title); // Обновляем название на русском
                        command.Parameters.AddWithValue("@TitleEng", titleEng); // Обновляем название на английском
                        command.Parameters.AddWithValue("@ReleaseYear", releaseYear); // Обновляем год выпуска
                        command.Parameters.AddWithValue("@Country", country); // Обновляем страну
                        command.Parameters.AddWithValue("@Genre", genre); // Обновляем жанр
                        command.Parameters.AddWithValue("@Description", description); // Обновляем описание
                        command.Parameters.AddWithValue("@Player", player); // Обновляем информацию о плеере
                        command.Parameters.AddWithValue("@ImagePath", imagePath); // Обновляем путь к изображению
                        command.Parameters.AddWithValue("@Actors", actors); // Обновляем актеров

                        command.ExecuteNonQuery();
                    }
                }

                // Перенаправляем на страницу контроля или на главную после успешного обновления
                return Redirect("/admin"); // Или другую страницу по вашему усмотрению
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обновлении фильма: {ex.Message}");
                return Json(new { success = false, message = "Не удалось обновить фильм" });
            }
        }

        [Post("admin/add-movie")]
        public IHttpResponseResult AddMovie(string title, string titleEng, string releaseYear, string country, string genre, string description, string filmLink, string imagePath, string actors, string directorName)
        {
            try
            {
                using (var sqlConnection = new SqlConnection("Data Source=localhost;Initial Catalog=User;User ID=sa;Password=P@ssw0rd"))
                {
                    sqlConnection.Open();

                    // Проверяем, есть ли такой режиссер в таблице Directors
                    int directorId;
                    string directorQuery = "SELECT Id FROM Directors WHERE Name = @Name";
                    using (var directorCommand = new SqlCommand(directorQuery, sqlConnection))
                    {
                        directorCommand.Parameters.AddWithValue("@Name", directorName);
                        var result = directorCommand.ExecuteScalar();
                        if (result != null)
                        {
                            directorId = Convert.ToInt32(result);
                        }
                        else
                        {
                            // Если режиссера нет, добавляем нового
                            string insertDirectorQuery = "INSERT INTO Directors (Name, Country, Age) OUTPUT INSERTED.Id VALUES (@Name, @Country, @Age)";

                            using (var insertDirectorCommand = new SqlCommand(insertDirectorQuery, sqlConnection))
                            {
                                insertDirectorCommand.Parameters.AddWithValue("@Name", directorName);

                                // Вам нужно будет также передать Country и Age. Возможно, вы их получаете где-то еще.
                                insertDirectorCommand.Parameters.AddWithValue("@Country", "Unknown"); // заполняйте реальным значением
                                insertDirectorCommand.Parameters.AddWithValue("@Age", 0); // или другим значением, если это подходит

                                directorId = (int)insertDirectorCommand.ExecuteScalar();
                            }
                        }
                    }

                    // Теперь добавляем фильм в таблицу Movies
                    string movieQuery = @"
             INSERT INTO Movies (Title, TitleEng, ReleaseYear, Country, Genre, Description, Player, ImagePath, Actors, DirectorId)
             VALUES (@Title, @TitleEng, @ReleaseYear, @Country, @Genre, @Description, @Player, @ImagePath, @Actors, @DirectorId)";

                    using (var command = new SqlCommand(movieQuery, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@Title", title);
                        command.Parameters.AddWithValue("@TitleEng", titleEng);
                        command.Parameters.AddWithValue("@ReleaseYear", releaseYear);
                        command.Parameters.AddWithValue("@Country", country);
                        command.Parameters.AddWithValue("@Genre", genre);
                        command.Parameters.AddWithValue("@Description", description);

                        command.Parameters.AddWithValue("@Player", filmLink);
                        command.Parameters.AddWithValue("@ImagePath", imagePath);
                        command.Parameters.AddWithValue("@Actors", actors);
                        command.Parameters.AddWithValue("@DirectorId", directorId); // связываем фильм с режиссером

                        command.ExecuteNonQuery();
                    }
                }

                // Перенаправляем после успешного добавления
                return Redirect("/admin");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении фильма: {ex.Message}");
                return Json(new { success = false, message = "Не удалось добавить фильм" });
            }
        }
    }
}