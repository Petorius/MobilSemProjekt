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
    public class DbLocation
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DBString"].ConnectionString;
        private readonly DbPicture _dbPicture;
        private readonly DbRating _dbRating;
        private readonly DbUser _dbUser;

        public DbLocation()
        {
            _dbPicture = new DbPicture();
            _dbRating = new DbRating();
            _dbUser = new DbUser();
        }

        public int Create(Location location)
        {
            int id;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Location(LocationName, Latitude, Longitude, UserId, LocationDescription) VALUES " +
                                      "(@LocationName, @Latitude, @Longitude, @UserId, @LocationDescription); ";
                    cmd.Parameters.AddWithValue("LocationName", location.LocationName);
                    cmd.Parameters.AddWithValue("Latitude", location.Latitude);
                    cmd.Parameters.AddWithValue("Longitude", location.Longitude);
                    cmd.Parameters.AddWithValue("LocationDescription", location.LocationDescription);
                    
                    if (location.User != null)
                    {
                        cmd.Parameters.AddWithValue("UserId", location.User.UserId);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("UserId", DBNull.Value);
                    }
                    
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return id;
        }
        /*
        public List<Location> FindByTagId(int tagId)
        {
            List<Location> locations = new List<Location>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Location WHERE TagId = @TagId";
                    cmd.Parameters.AddWithValue("TagId", tagId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int locationId = reader.GetInt32(reader.GetOrdinal("LocationId"));
                        int userId = reader.GetInt32(reader.GetOrdinal("LocationId"));

                        Location location = new Location
                        {
                            LocationId = locationId,
                            LocationName = reader.GetString(reader.GetOrdinal("LocationName")),
                            LocationDescription = reader.GetString(reader.GetOrdinal("LocationDescription")),
                            Latitude = reader.GetDouble(reader.GetOrdinal("Latitude")),
                            Longitude = reader.GetDouble(reader.GetOrdinal("Longitude")),
                            Pictures = _dbPicture.FindByLocationId(locationId),
                            Ratings = _dbRating.FindByLocationId(locationId),
                            User = _dbUser.FindById(userId)
                        };

                        locations.Add(location);
                    }
                }
            }

            return locations;
        }

*/

        public Location FindById(int locationId)
        {
            Location location = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Location WHERE LocationId = @LocationId";
                    cmd.Parameters.AddWithValue("LocationId", locationId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        location = new Location
                        {
                            LocationId = locationId,
                            LocationName = reader.GetString(reader.GetOrdinal("LocationName")),
                            LocationDescription = reader.GetString(reader.GetOrdinal("LocationDescription")),
                            Latitude = reader.GetDouble(reader.GetOrdinal("Latitude")),
                            Longitude = reader.GetDouble(reader.GetOrdinal("Longitude")),
                            Pictures = _dbPicture.FindByLocationId(locationId),
                            Ratings = _dbRating.FindByLocationId(locationId)
                        };
                    }
                }
            }
            return location;
        }

        public Location FindByName(string locationName)
        {
            Location location = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Location WHERE LocationName = @LocationName";
                    cmd.Parameters.AddWithValue("LocationName", locationName);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int locationId = reader.GetInt32(reader.GetOrdinal("LocationId"));
                        location = new Location
                        {
                            LocationId = locationId,
                            LocationName = locationName,
                            LocationDescription = reader.GetString(reader.GetOrdinal("LocationDescription")),
                            Latitude = reader.GetDouble(reader.GetOrdinal("Latitude")),
                            Longitude = reader.GetDouble(reader.GetOrdinal("Longitude")),
                            Pictures = _dbPicture.FindByLocationId(locationId),
                            Ratings = _dbRating.FindByLocationId(locationId)
                        };
                    }
                }
            }
            return location;
        }

        public void Update(Location location, int locationId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Location set LocationName = @LocationName, Latitude = @Latitude, " +
                                      "Longitude = @Longitude, UserId = @UserId, LocationDescription = @LocationDescription " +
                                      "where LocationId = @LocationId";
                    cmd.Parameters.AddWithValue("LocationId", locationId);
                    cmd.Parameters.AddWithValue("LocationName", location.LocationName);
                    cmd.Parameters.AddWithValue("Latitude", location.Latitude);
                    cmd.Parameters.AddWithValue("Longitude", location.Longitude);
                    cmd.Parameters.AddWithValue("UserId", location.User.UserId);
                    cmd.Parameters.AddWithValue("LocationDescription", location.LocationDescription);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int locationId)
        {
            int changes;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Location where LocationId = @LocationId";
                    cmd.Parameters.AddWithValue("LocationId", locationId);
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
