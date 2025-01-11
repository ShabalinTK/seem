//using HttpServerLibrary;
//using HttpServerLibrary.Attributes;
//using HttpServerLibrary.HttpResponse;
//using MyHttpServer.Models;
//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MyHttpServer.Endpoints
//{
//    public class UnitEndpoin : EndpointBase
//    {
//        [Get("users")]
//        public IHttpResponseResult GetUsers()
//        {
//            //var users = new List<User>();

//            string connectionString = @"Data Source=localhost;Initial Catalog=User;User ID=sa;Password=P@ssw0rd;";

//            string sqlExpression = "SELECT * FROM User";
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                SqlCommand command = new SqlCommand(sqlExpression, connection);
//                SqlDataReader reader = command.ExecuteReader();

//                if (reader.HasRows) // если есть данные
//                {
//                    // выводим названия столбцов
//                    Console.WriteLine("{0}\t{1}\t{2}", reader.GetName(0), reader.GetName(1), reader.GetName(2));

//                    while (reader.Read()) // построчно считываем данные
//                    {
//                        //var user = new User()
//                        //{
//                        //    Id = reader.GetInt32(0),
//                        //    Login = reader.GetString(1),
//                        //    Password = reader.GetString(2)
//                        //};

//                        //users.Add(user);
//                    }
//                }

//                reader.Close();
//            }
//            //return Json(users);
//        }
//    }
//}