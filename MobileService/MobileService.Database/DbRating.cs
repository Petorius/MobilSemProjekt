using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using MobileService.Model;

namespace MobileService.Database
{
    public class DbRating
    {
        private readonly DbUser _dbUser;
        private static SqlConnection _connection;
        private readonly string _connectionString = "Server=kraka.ucn.dk;" +
                                                    "Database=dmaa0917_1067347;User ID=dmaa0917_1067347;" +
                                                    "Password=Password1!;";

        public DbRating()
        {
            _dbUser = new DbUser();
        }

        public int Create(Rating rating)
        {
            int id;

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();

                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Rating(rate, comment, locationId, userId) VALUES " +
                                      "(@rate, @comment, @locationId, @userId); ";
                    cmd.Parameters.AddWithValue("rate", rating.Rate);
                    cmd.Parameters.AddWithValue("comment", rating.Comment);
                    
                    if (rating.User != null)
                    {
                        cmd.Parameters.AddWithValue("userId", rating.User.UserId);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("userId", DBNull.Value);
                    }
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
                _connection.Close();
            }
            return id;
        }

        public Rating FindById(int ratingId)
        {
            Rating rating = null;

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Rating WHERE RatingId = @RatingId";
                    cmd.Parameters.AddWithValue("RatingId", ratingId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int userId = reader.GetInt32(reader.GetOrdinal("UserId"));

                        rating = new Rating
                        {
                            RatingId = ratingId,
                            User = _dbUser.FindById(userId),
                            Rate = reader.GetDouble(reader.GetOrdinal("Rate")),
                            Comment = reader.GetString(reader.GetOrdinal("Comment"))
                        };
                    }
                }
                _connection.Close();
            }
            return rating;
        }

        public List<Rating> FindByLocationId(int locationId)
        {
            List<Rating> ratings = new List<Rating>();

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Rating WHERE LocationId = @LocationId";
                    cmd.Parameters.AddWithValue("LocationId", locationId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = null;
                        
                        if (!reader.IsDBNull(reader.GetOrdinal("UserId")))
                        {
                            int userId = reader.GetInt32(reader.GetOrdinal("UserId"));
                            user = _dbUser.FindById(userId);
                        }

                        Rating rating = new Rating
                        {
                            RatingId = reader.GetInt32(reader.GetOrdinal("RatingId")),
                            User = user,
                            Rate = reader.GetDouble(reader.GetOrdinal("Rate")),
                            Comment = reader.GetString(reader.GetOrdinal("Comment"))
                        };
                        ratings.Add(rating);
                    }
                }
                _connection.Close();
            }
            return ratings;
        }

        public List<Rating> FindByUserId(int userId)
        {
            List<Rating> ratings = new List<Rating>();

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Rating WHERE userId = @userId";
                    cmd.Parameters.AddWithValue("userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = _dbUser.FindById(userId);
                        
                        Rating rating = new Rating
                        {
                            RatingId = reader.GetInt32(reader.GetOrdinal("id")),
                            User = user,
                            Rate = reader.GetDouble(reader.GetOrdinal("rate")),
                            Comment = reader.GetString(reader.GetOrdinal("comment"))
                        };
                        ratings.Add(rating);
                    }
                }
                _connection.Close();
            }
            return ratings;
        }

        public void Delete(int ratingId)
        {
            int changes;

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Rating where RatingId = @RatingId";
                    cmd.Parameters.AddWithValue("RatingId", ratingId);
                    changes = cmd.ExecuteNonQuery();
                }
                _connection.Close();
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