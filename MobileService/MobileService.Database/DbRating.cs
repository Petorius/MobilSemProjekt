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
    public class DbRating
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DBString"].ConnectionString;
        private DbLocation _dbLocation;
        private DbUser _dbUser;

        public DbRating()
        {
            _dbLocation = new DbLocation();
            _dbUser = new DbUser();
        }

        public int Create(Rating rating)
        {
            int id;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Rating(rate, comment) VALUES " +
                                      "(@rate, @comment); ";
                    cmd.Parameters.AddWithValue("rate", rating.Rate);
                    cmd.Parameters.AddWithValue("comment", rating.Comment);
                    if (rating.Location != null)
                    {
                        cmd.Parameters.AddWithValue("location", rating.Location.LocationId);
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
            }
            return id;
        }

        public Rating FindById(int ratingId)
        {
            Rating rating = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Rating WHERE RatingId = @RatingId";
                    cmd.Parameters.AddWithValue("RatingId", ratingId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int userId = reader.GetInt32(reader.GetOrdinal("UserId"));
                        int locationId = reader.GetInt32(reader.GetOrdinal("LocationId"));

                        rating = new Rating
                        {
                            RatingId = ratingId,
                            Location = _dbLocation.FindById(locationId),
                            User = _dbUser.FindById(userId),
                            Rate = reader.GetDouble(reader.GetOrdinal("Rate")),
                            Comment = reader.GetString(reader.GetOrdinal("Comment"))
                        };
                    }
                }
            }
            return rating;
        }

        public List<Rating> FindByLocationId(int locationId)
        {
            List<Rating> ratings = new List<Rating>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Rating WHERE locationId = @locationId";
                    cmd.Parameters.AddWithValue("locationId", locationId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Location location = _dbLocation.FindById(locationId);
                        User user = null;
                        
                        if (!reader.IsDBNull(reader.GetOrdinal("userId")))
                        {
                            int userId = reader.GetInt32(reader.GetOrdinal("userId"));
                            user = _dbUser.FindById(userId);
                        }

                        Rating rating = new Rating
                        {
                            RatingId = reader.GetInt32(reader.GetOrdinal("id")),
                            Location = location,
                            User = user,
                            Rate = reader.GetDouble(reader.GetOrdinal("rate")),
                            Comment = reader.GetString(reader.GetOrdinal("comment"))
                        };
                        ratings.Add(rating);
                    }
                }
            }
            return ratings;
        }

        public List<Rating> FindByUserId(int userId)
        {
            List<Rating> ratings = new List<Rating>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Rating WHERE userId = @userId";
                    cmd.Parameters.AddWithValue("userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Location location = null;
                        User user = _dbUser.FindById(userId);

                        if (!reader.IsDBNull(reader.GetOrdinal("locationId")))
                        {
                            int locationId = reader.GetInt32(reader.GetOrdinal("locationId"));
                            location = _dbLocation.FindById(locationId);
                        }

                        Rating rating = new Rating
                        {
                            RatingId = reader.GetInt32(reader.GetOrdinal("id")),
                            Location = location,
                            User = user,
                            Rate = reader.GetDouble(reader.GetOrdinal("rate")),
                            Comment = reader.GetString(reader.GetOrdinal("comment"))
                        };
                        ratings.Add(rating);
                    }
                }
            }
            return ratings;
        }
        
    }
}
