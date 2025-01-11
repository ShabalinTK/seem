using System.Data.SqlClient;

namespace MyORMLibrary;

public class ORMContext
{
    private readonly string _connectionString;

    public SqlConnection SqlConnection { get; set; }

    public ORMContext(string connectionString)
    {
        _connectionString = connectionString;
        SqlConnection = new 
    }

    public T Create<T>(T entity, string tableName) where T : class
    {
        // Пример реализации метода Create
        // Параметризованный SQL-запрос для вставки данных
        throw new NotImplementedException();
    }

    public T MapData<T>(SqlDataReader reader) where T : class, new()
    {
        var obj = new T();
        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            if (!reader.HasRows || reader[property.Name] == DBNull.Value) continue;

            property.SetValue(obj, reader[property.Name]);
        }

        return obj;
    }

    public T ReadById<T>(int id, string tableName) where T : class, new()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sql = $"SELECT * FROM {tableName} WHERE Id = @id";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return MapData<T>(reader);
                }
            }
        }

        return null;
    }

    public List<T> ReadByAll<T>(string tableName) where T : class, new()
    {
        var resultList = new List<T>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sql = $"SELECT * FROM {tableName}";
            SqlCommand command = new SqlCommand(sql, connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    resultList.Add(MapData<T>(reader));
                }
            }
        }

        return resultList;
    }

    public List<T> ReadWithFilter<T>(string tableName, string filter, Dictionary<string, object> parameters) where T : class, new()
    {
        var resultList = new List<T>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sql = $"SELECT * FROM {tableName} WHERE {filter}";
            SqlCommand command = new SqlCommand(sql, connection);

            foreach (var param in parameters)
            {
                command.Parameters.AddWithValue(param.Key, param.Value);
            }

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    resultList.Add(MapData<T>(reader));
                }
            }
        }

        return resultList;
    }


    public void Update<T>(int id, T entity, string tableName)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sql = $"UPDATE {tableName} SET Column1 = @value1 WHERE Id = @id";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@value1", "значение");

            command.ExecuteNonQuery();
        }
    }

    public void Delete(int id, string tableName)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sql = $"DELETE FROM {tableName} WHERE Id = @id";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }
    }
}
