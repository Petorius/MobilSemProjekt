using System;
using System.Data.SqlClient;
using MobileService.Model;

namespace MobileService.Database
{
    public class DbUserType
    {
        private static SqlConnection _connection;
        private readonly string _connectionString = "Server=kraka.ucn.dk;" +
                        "Database=dmaa0917_1067347;User ID=dmaa0917_1067347;" +
                        "Password=Password1!;";

        public int Create(UserType userType)
        {
            int id;

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();

                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO UserType(TypeName) VALUES (@TypeName); ";
                    cmd.Parameters.AddWithValue("TypeName", userType.TypeName);
                    
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
                _connection.Close();
            }
            return id;
        }

        public UserType FindById(int userTypeId)
        {
            UserType userType = null;

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM UserType WHERE UserTypeId = @UserTypeId";
                    cmd.Parameters.AddWithValue("UserTypeId", userTypeId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        userType = new UserType
                        {
                            UserTypeId = userTypeId,
                            TypeName = reader.GetString(reader.GetOrdinal("TypeName"))
                        };
                    }
                }
                _connection.Close();
            }
            return userType;
        }
    }
}