using System;
using System.Data.SqlClient;
using System.ServiceModel;
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

        private readonly DbUserType _dbUserType;

        public DbUser()
        {
            _dbUserType = new DbUserType();
        }
        
        /// <summary>
        /// Create a user in database
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>int</returns>
        public int Create(User user)
        {
            int id;
            
            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();

                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO [User](UserName, HashPassword, Salt, UserTypeId) VALUES " +
                                      "(@UserName, @HashPassword, @Salt, @UserTypeId); SELECT SCOPE_IDENTITY()";
                    cmd.Parameters.AddWithValue("UserName", user.UserName);
                    cmd.Parameters.AddWithValue("HashPassword", user.HashPassword);
                    cmd.Parameters.AddWithValue("Salt", user.Salt);
                    cmd.Parameters.AddWithValue("UserTypeId", user.UserType.UserTypeId);

                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
                _connection.Close();
            }
            return id;
        }

        /// <summary>
        /// Find a user based on its id in database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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
                        int userTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId"));
                        user = new User
                        {
                            UserId = userId,
                            UserName = reader.GetString(reader.GetOrdinal("UserName")),
                            HashPassword = reader.GetString(reader.GetOrdinal("HashPassword")),
                            Salt = reader.GetString(reader.GetOrdinal("Salt")),
                            UserType = _dbUserType.FindById(userTypeId)
                        };

                    }
                }
                _connection.Close();
            }

            if (user == null)
            {
                throw new FaultException<UserNotFoundException>(new UserNotFoundException(""));
            }
            return user;
        }

        /// <summary>
        /// Find a user based on its username in database
        /// If user tries to login it throws a login error
        /// </summary>
        /// <param name="userName">string</param>
        /// <param name="login">bool</param>
        /// <returns>User</returns>
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
                        int userTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId"));

                        user = new User
                        {
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            UserName = userName,
                            HashPassword = reader.GetString(reader.GetOrdinal("HashPassword")),
                            Salt = reader.GetString(reader.GetOrdinal("Salt")),
                            UserType = _dbUserType.FindById(userTypeId)
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

        /// <summary>
        /// Find a user based on its username in database
        /// </summary>
        /// <param name="userName">string</param>
        /// <returns>int</returns>
        public int FindIdByUserName(string userName)
        {
            int userId = 0;

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
                        userId = reader.GetInt32(reader.GetOrdinal("UserId"));
                    }
                }
                _connection.Close();
            }
            return userId;
        }

        /// <summary>
        /// Update a user in database
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>bool</returns>
        public bool Update(User user)
        {
            int changes;

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE [User] SET UserName = @UserName, HashPassword = @HashPassword, " +
                                      "Salt = @Salt,  UserTypeId = @UserTypeId WHERE UserId = @UserId";
                    cmd.Parameters.AddWithValue("UserName", user.UserName);
                    cmd.Parameters.AddWithValue("HashPassword", user.HashPassword);
                    cmd.Parameters.AddWithValue("Salt", user.Salt);
                    cmd.Parameters.AddWithValue("UserTypeId", user.UserType.UserTypeId);
                    cmd.Parameters.AddWithValue("UserId", user.UserId);
                    changes = cmd.ExecuteNonQuery();
                }
                _connection.Close();
            }
            
            return changes > 0;
        }

        /// <summary>
        /// Delete a user in database
        /// </summary>
        /// <param name="userName">string</param>
        public void Delete(string userName)
        {
            int changes;

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM [User] WHERE UserName = @UserName";
                    cmd.Parameters.AddWithValue("UserName", userName);
                    changes = cmd.ExecuteNonQuery();
                }
                _connection.Close();
            }

            bool status = changes > 0;
            if (status == false)
            {
                throw new FaultException<UserNotDeletedException>(new UserNotDeletedException(userName));
            }
        }
    }
}
