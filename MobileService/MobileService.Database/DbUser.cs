using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileService.Model;

namespace MobileService.Database
{
    public class DbUser
    {
        public int Create(User user)
        {
            DbConnection dbc = new DbConnection();
            SqlConnection sqlC = dbc.Connection;
            int id;

            using (sqlC)
            {
                sqlC.Open();

                using (SqlCommand cmd = sqlC.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO [User](rate, comment) VALUES " +
                                      "(@rate, @comment); ";
                    cmd.Parameters.AddWithValue("UserName", user.UserName);
                    cmd.Parameters.AddWithValue("HashPassword", user.HashPassword);
                    cmd.Parameters.AddWithValue("Salt", user.Salt);
                    
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return id;
        }

        public User FindById(int userId)
        {
            DbConnection dbc = new DbConnection();
            SqlConnection sqlC = dbc.Connection;
            User user = null;

            using (sqlC)
            {
                sqlC.Open();
                using (SqlCommand cmd = sqlC.CreateCommand())
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
            }
            return user;
        }

        public User FindByName(string userName)
        {
            DbConnection dbc = new DbConnection();
            SqlConnection sqlC = dbc.Connection;
            User user = null;

            using (sqlC)
            {
                sqlC.Open();
                using (SqlCommand cmd = sqlC.CreateCommand())
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
                }
            }
            return user;
        }

        public void Delete(int userId)
        {
            DbConnection dbc = new DbConnection();
            SqlConnection sqlC = dbc.Connection;
            int changes;

            using (sqlC)
            {
                sqlC.Open();
                using (SqlCommand cmd = sqlC.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM [User] where UserId = @UserId";
                    cmd.Parameters.AddWithValue("UserId", userId);
                    changes = cmd.ExecuteNonQuery();
                }
            }

            bool status = changes > 0;
            if (status == false)
            {
                throw new Exception();
                //throw new FaultException<CustomerNotDeletedException>(new CustomerNotDeletedException(customer._phone));
            }
        }
    }
}
