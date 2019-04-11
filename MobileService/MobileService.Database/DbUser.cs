using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MobileService.Exception;
using MobileService.Model;

namespace MobileService.Database
{
    public class DbUser
    {
        private static SqlConnection _connection;
        private readonly string _connectionString = "Server=kraka.ucn.dk;" +
                        "Database=dmaa0917_1067347;User ID=dmaa0917_1067347;" +
                        "Password=Password1!;";

        public int Create(User user)
        {
            int id;

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();

                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO [User](rate, comment) VALUES " +
                                      "(@rate, @comment); ";
                    cmd.Parameters.AddWithValue("UserName", user.UserName);
                    cmd.Parameters.AddWithValue("HashPassword", user.HashPassword);
                    cmd.Parameters.AddWithValue("Salt", user.Salt);
                    
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
                _connection.Close();
            }
            return id;
        }

        public User FindById(int userId)
        {
            User user = null;

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM [User] WHERE UserId = @UserId";
                    cmd.Parameters.AddWithValue("UserId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user = new User
                        {
                            UserId = userId,
                            UserName = reader.GetString(reader.GetOrdinal("UserName")),
                            HashPassword = reader.GetString(reader.GetOrdinal("HashPassword")),
                            Salt = reader.GetString(reader.GetOrdinal("Salt"))
                        };
                    }
                }
                _connection.Close();
            }
            return user;
        }

        public User FindUserByUserName(string userName, bool login)
        {
            User user = null;

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM [User] WHERE UserName = @UserName";
                    cmd.Parameters.AddWithValue("UserName", userName);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user = new User
                        {
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            UserName = userName,
                            HashPassword = reader.GetString(reader.GetOrdinal("HashPassword")),
                            Salt = reader.GetString(reader.GetOrdinal("Salt"))
                        };
                    }

                    if (user == null)
                    {
                        if (login)
                        {
                            throw new FaultException<UserOrPasswordException>(new UserOrPasswordException());
                        }
                        throw new FaultException<UserNotFoundException>(new UserNotFoundException(userName));
                    }
                }
                _connection.Close();
            }
            return user;
        }

        public void Delete(int userId)
        {
            int changes;

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM [User] where UserId = @UserId";
                    cmd.Parameters.AddWithValue("UserId", userId);
                    changes = cmd.ExecuteNonQuery();
                }
                _connection.Close();
            }

            bool status = changes > 0;
            if (status == false)
            {
                throw new System.Exception();
                //throw new FaultException<CustomerNotDeletedException>(new CustomerNotDeletedException(customer._phone));
            }
        }
    }
}
