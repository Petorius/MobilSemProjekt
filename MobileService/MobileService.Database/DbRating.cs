using System;
using System.Collections.Generic;
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
                    
                    int locId = rating.LocationId;
                    if (locId != 0)
                    {
                        cmd.Parameters.AddWithValue("locationId", locId);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("locationId", DBNull.Value);
                    }

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
                            Comment = reader.GetString(reader.GetOrdinal("Comment")),
                            LocationId = locationId
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

        public List<int> FindLocationIdsByUserName(string username)
        {
            List<int> locationIds = new List<int>();
            User user = _dbUser.FindUserByUserName(username, false);

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT LocationId FROM Rating WHERE userId = @userId";
                    cmd.Parameters.AddWithValue("userId", user.UserId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int locationId = reader.GetInt32(reader.GetOrdinal("LocationId"));
                        if (!locationIds.Contains(locationId))
                        {
                            locationIds.Add(locationId);
                        }
                    }
                }
                _connection.Close();
            }
            return locationIds;
        }

        public List<Rating> FindRatingsByUsername(string username)
        {
            List<Rating> ratings = new List<Rating>();
            User user = _dbUser.FindUserByUserName(username, false);

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Rating WHERE userId = @userId";
                    cmd.Parameters.AddWithValue("userId", user.UserId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
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

        public double GetAverageRating(int locationId)
        {
            double avgValue = 0;
            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT AVG(rate) as avgRate FROM Rating WHERE LocationId = @locationId";
                    cmd.Parameters.AddWithValue("LocationId", locationId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("avgRate")))
                        {
                            avgValue = reader.GetDouble(reader.GetOrdinal("avgRate"));
                        }
                        else
                        {
                            avgValue = 0;
                        }
                    }
                }
                _connection.Close();
            }
            return avgValue;
        }

        public bool Update(Rating rating)
        {
            int changes = 0;
            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Rating set Rate = @Rate, Comment = @Comment, " +
                                      "LocationId = @LocationId, UserId = @UserId where RatingId = @RatingId";
                    cmd.Parameters.AddWithValue("Rate", rating.Rate);
                    cmd.Parameters.AddWithValue("Comment", rating.Comment);
                    cmd.Parameters.AddWithValue("UserId", rating.User.UserId);
                    cmd.Parameters.AddWithValue("RatingId", rating.RatingId);
                    changes = cmd.ExecuteNonQuery();
                }
                _connection.Close();
            }

            return changes > 0;
        }

        public bool UserUpdateRating(Rating rating)
        {
            int changes = 0;
            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Rating set Rate = @Rate, Comment = @Comment where RatingId = @RatingId";
                    cmd.Parameters.AddWithValue("Rate", rating.Rate);
                    cmd.Parameters.AddWithValue("Comment", rating.Comment);
                    cmd.Parameters.AddWithValue("RatingId", rating.RatingId);
                    changes = cmd.ExecuteNonQuery();
                }
                _connection.Close();
            }

            return changes > 0;
        }

        public bool Delete(Rating rating)
        {
            int changes;
            int ratingId = rating.RatingId;

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

            return changes > 0;
        }
    }
}