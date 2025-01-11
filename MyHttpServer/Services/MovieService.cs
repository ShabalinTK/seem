using MyHttpServer.Models;
using System.Data.SqlClient;

namespace MyHttpServer.Services;

internal class MovieService
{
    public Movie GetMovieWithDetails(int movieId)
    {
        using var connection = new SqlConnection(@"Data Source=localhost;Initial Catalog=User;User ID=sa;Password=P@ssw0rd");
        connection.Open();

        // Получаем основные данные фильма
        string movieQuery = "SELECT * FROM Movies WHERE Id = @Id";
        Movie movie = null;

        using (var command = new SqlCommand(movieQuery, connection))
        {
            command.Parameters.AddWithValue("@Id", movieId);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    movie = new Movie
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        ReleaseYear = reader.GetString(reader.GetOrdinal("ReleaseYear")),
                        Genre = reader.GetString(reader.GetOrdinal("Genre")),
                        Description = reader.GetString(reader.GetOrdinal("Description")),
                        Country = reader.GetString(reader.GetOrdinal("Country")),
                        Player = reader.GetString(reader.GetOrdinal("Player")),
                        ImagePath = reader.GetString(reader.GetOrdinal("ImagePath")),
                        Actors = reader.GetString(reader.GetOrdinal("Actors")),
                    };
                }
            }
        }

        if (movie == null) return null;

        // Получаем режиссёра
        string directorQuery = @"
        SELECT d.Id, d.Name, d.Country, d.Age
        FROM Directors d
        WHERE d.Id = (SELECT m.DirectorId FROM Movies m WHERE m.Id = @MovieId)";
        using (var command = new SqlCommand(directorQuery, connection))
        {
            command.Parameters.AddWithValue("@MovieId", movieId);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    movie.Director = new Director
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                    };
                }
            }
        }

        // Получаем статистику лайков и дизлайков
        string statsQuery = @"SELECT 
                                SUM(CASE WHEN IsLike = 1 THEN 1 ELSE 0 END) as Likes,
                                SUM(CASE WHEN IsLike = 0 THEN 1 ELSE 0 END) as Dislikes
                                 FROM MovieStats
                                 WHERE MovieId = @MovieId";

        using (var command = new SqlCommand(statsQuery, connection))
        {
            command.Parameters.AddWithValue("@MovieId", movieId);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    movie.LikesCount = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    movie.DislikesCount = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                }
            }
        }

        return movie;
    }
}