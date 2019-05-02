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


        /// <summary>
        /// Create a type of users in the database
        /// </summary>
        /// <param name="userType">UserType</param>
        /// <returns>int</returns>
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

        /// <summary>
        /// Find a type of users based on its id in the database
        /// </summary>
        /// <param name="userTypeId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Find a type of users based on its name in database
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public int FindIdByName(string typeName)
        {
            int userTypeId = 0;

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT userTypeId FROM UserType WHERE TypeName = @TypeName";
                    cmd.Parameters.AddWithValue("TypeName", typeName);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        userTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId"));
                    }
                }
                _connection.Close();
            }
            return userTypeId;
        }
    }
}