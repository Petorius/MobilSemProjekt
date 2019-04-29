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

        private DbUserType DbUserType;

        public DbUser()
        {
            DbUserType = new DbUserType();
        }

        private int FindUserTypeBasedOnKnowledge(UserType userType)
        {
            int userTypeId = 0;
            if (userType.UserTypeId != 0)
            {
                userTypeId = userType.UserTypeId;
            }
            else
            {
                userTypeId = DbUserType.FindIdByName(userType.TypeName);
            }

            return userTypeId;
        }

        public int Create(User user)
        {
            int id;
            int userTypeId = FindUserTypeBasedOnKnowledge(user.UserType);
            
            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();

                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO [User](UserName, HashPassword, Salt, UserTypeId) VALUES " +
                                      "(@UserName, @HashPassword, @Salt, @UserTypeId); ";
                    cmd.Parameters.AddWithValue("UserName", user.UserName);
                    cmd.Parameters.AddWithValue("HashPassword", user.HashPassword);
                    cmd.Parameters.AddWithValue("Salt", user.Salt);
                    cmd.Parameters.AddWithValue("UserTypeId", userTypeId);

                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
                _connection.Close();
            }
            return id;
        }

        public User FindById(int userId)
        {
            User user = null;
            int userTypeId = FindUserTypeBasedOnKnowledge(user.UserType);

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
                            Salt = reader.GetString(reader.GetOrdinal("Salt")),
                            UserType = DbUserType.FindById(userTypeId)
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
            int userTypeId = FindUserTypeBasedOnKnowledge(user.UserType);

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
                            Salt = reader.GetString(reader.GetOrdinal("Salt")),
                            UserType = DbUserType.FindById(userTypeId)
                        };
                    }

                    if (user == null)
                    {
                        if (login)
                        {
                            throw new FaultException<UserOrPasswordException>(new UserOrPasswordException());
                        }
                        throw new FaultException<UserNotDeletedException>(new UserNotDeletedException(userName));
                    }
                }
                _connection.Close();
            }
            return user;
        }
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
