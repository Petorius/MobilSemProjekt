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
        private readonly DbPicture _dbPicture;
        private readonly DbRating _dbRating;
        private readonly DbUser _dbUser;
        private static SqlConnection _connection;
        private readonly string _connectionString = "Server=kraka.ucn.dk;" +
                                                    "Database=dmaa0917_1067347;User ID=dmaa0917_1067347;" +
                                                    "Password=Password1!;";

        public DbLocation()
        {
            _dbPicture = new DbPicture();
            _dbRating = new DbRating();
            _dbUser = new DbUser();
        }

        public int Create(Location location)
        {
            int locationId;
            
            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();

                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Locations(Hits, IsTopLocation, LocationName, Latitude, Longitude, UserId, LocationDescription) VALUES " +
                                      "(@Hits, @IsTopLocation, @LocationName, @Latitude, @Longitude, @UserId, @LocationDescription); ";
                    cmd.Parameters.AddWithValue("Hits", location.Hits);
                    cmd.Parameters.AddWithValue("IsTopLocation", location.IsTopLocation);
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
                    
                    locationId = Convert.ToInt32(cmd.ExecuteScalar());

                    foreach (Picture picture in location.Pictures)
                    {
                        _dbPicture.Create(picture, locationId);
                    }
                }
                _connection.Close();
            }
            return locationId;
        }
        
        public Location FindById(int locationId)
        {
            Location location = null;
            
            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Locations WHERE LocationId = @LocationId";
                    cmd.Parameters.AddWithValue("LocationId", locationId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        location = new Location
                        {
                            LocationId = locationId,
                            Hits = reader.GetInt32(reader.GetOrdinal("Hits")),
                            IsTopLocation = reader.GetBoolean(reader.GetOrdinal("IsTopLocation")),
                            LocationName = reader.GetString(reader.GetOrdinal("LocationName")),
                            LocationDescription = reader.GetString(reader.GetOrdinal("LocationDescription")),
                            Latitude = reader.GetDouble(reader.GetOrdinal("Latitude")),
                            Longitude = reader.GetDouble(reader.GetOrdinal("Longitude")),
                            Pictures = _dbPicture.FindByLocationId(locationId),
                            Ratings = _dbRating.FindByLocationId(locationId),
                            User = _dbUser.FindById(reader.GetOrdinal("UserId")),
                            RowVersion = (byte[])reader.GetValue(reader.GetOrdinal("rowVersion"))
                        };
                    }
                }
                _connection.Close();
            }
            return location;
        }

        public Location FindByName(string locationName)
        {
            Location location = null;

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Locations WHERE LocationName = @LocationName";
                    cmd.Parameters.AddWithValue("LocationName", locationName);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int locationId = reader.GetInt32(reader.GetOrdinal("LocationId"));
                        location = new Location
                        {
                            LocationId = locationId,
                            LocationName = locationName,
                            Hits = reader.GetInt32(reader.GetOrdinal("Hits")),
                            IsTopLocation = reader.GetBoolean(reader.GetOrdinal("IsTopLocation")),
                            LocationDescription = reader.GetString(reader.GetOrdinal("LocationDescription")),
                            Latitude = reader.GetDouble(reader.GetOrdinal("Latitude")),
                            Longitude = reader.GetDouble(reader.GetOrdinal("Longitude")),
                            Pictures = _dbPicture.FindByLocationId(locationId),
                            Ratings = _dbRating.FindByLocationId(locationId),
                            User = _dbUser.FindById(reader.GetOrdinal("UserId")),
                            RowVersion = (byte[]) reader.GetValue(reader.GetOrdinal("rowVersion"))
                        };
                    }
                }
                _connection.Close();
            }
            return location;
        }

        public List<Location> FindLocationsByUserName(string userName)
        {
            List<Location> locations = new List<Location>();
            User user = _dbUser.FindUserByUserName(userName, false);
            if (user != null)
            {
                int userId = user.UserId;
                using (_connection = new SqlConnection(_connectionString))
                {
                    _connection.Open();
                    using (SqlCommand cmd = _connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM Locations WHERE UserId = @UserId";
                        cmd.Parameters.AddWithValue("UserId", userId);
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            int locationId = reader.GetInt32(reader.GetOrdinal("LocationId"));
                            Location location = new Location
                            {
                                LocationId = locationId,
                                Hits = reader.GetInt32(reader.GetOrdinal("Hits")),
                                IsTopLocation = reader.GetBoolean(reader.GetOrdinal("IsTopLocation")),
                                LocationName = reader.GetString(reader.GetOrdinal("LocationName")),
                                LocationDescription = reader.GetString(reader.GetOrdinal("LocationDescription")),
                                Latitude = reader.GetDouble(reader.GetOrdinal("Latitude")),
                                Longitude = reader.GetDouble(reader.GetOrdinal("Longitude")),
                                Pictures = _dbPicture.FindByLocationId(locationId),
                                Ratings = _dbRating.FindByLocationId(locationId),
                                User = user,
                                RowVersion = (byte[])reader.GetValue(reader.GetOrdinal("rowVersion"))
                            };
                            locations.Add(location);
                        }
                    }
                    _connection.Close();
                }
            }

            return locations;
        }

        public List<Location> LocationsByCommentUserName(string userName)
        {
            List<int> listOfLocationId = _dbRating.FindLocationIdsByUserName(userName);
            
            List<Location> locations = new List<Location>();
            foreach (int locationId in listOfLocationId)
            {
                using (_connection = new SqlConnection(_connectionString))
                {
                    _connection.Open();
                    using (SqlCommand cmd = _connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM Locations WHERE LocationId = @LocationId";
                        cmd.Parameters.AddWithValue("LocationId", locationId);
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Location location = new Location
                            {
                                LocationId = locationId,
                                Hits = reader.GetInt32(reader.GetOrdinal("Hits")),
                                IsTopLocation = reader.GetBoolean(reader.GetOrdinal("IsTopLocation")),
                                LocationName = reader.GetString(reader.GetOrdinal("LocationName")),
                                LocationDescription = reader.GetString(reader.GetOrdinal("LocationDescription")),
                                Latitude = reader.GetDouble(reader.GetOrdinal("Latitude")),
                                Longitude = reader.GetDouble(reader.GetOrdinal("Longitude")),
                                Pictures = _dbPicture.FindByLocationId(locationId),
                                Ratings = _dbRating.FindByLocationId(locationId),
                                User = _dbUser.FindUserByUserName(userName, false),
                                RowVersion = (byte[])reader.GetValue(reader.GetOrdinal("rowVersion"))
                            };
                            locations.Add(location);
                        }
                    }
                }
                _connection.Close();
            }

            return locations;
        }

        public List<Location> FindAll()
        {
            List<Location> locations = new List<Location>();

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Locations";
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int locationId = reader.GetInt32(reader.GetOrdinal("LocationId"));
                        int userId = reader.GetInt32(reader.GetOrdinal("LocationId"));

                        Location location = new Location
                        {
                            LocationId = locationId,
                            Hits = reader.GetInt32(reader.GetOrdinal("Hits")),
                            IsTopLocation = reader.GetBoolean(reader.GetOrdinal("IsTopLocation")),
                            LocationName = reader.GetString(reader.GetOrdinal("LocationName")),
                            LocationDescription = reader.GetString(reader.GetOrdinal("LocationDescription")),
                            Latitude = reader.GetDouble(reader.GetOrdinal("Latitude")),
                            Longitude = reader.GetDouble(reader.GetOrdinal("Longitude")),
                            Pictures = _dbPicture.FindByLocationId(locationId),
                            Ratings = _dbRating.FindByLocationId(locationId),
                            User = _dbUser.FindById(userId),
                            RowVersion = (byte[])reader.GetValue(reader.GetOrdinal("rowVersion"))
                        };

                        locations.Add(location);
                    }
                }
                _connection.Close();
            }

            return locations;
        }

        public void UpdateLocation(Location location)
        {
            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Locations set Hits = @Hits, IsTopLocation = @IsTopLocation, LocationName = @LocationName, Latitude = @Latitude, " +
                                      "Longitude = @Longitude, UserId = @UserId, LocationDescription = @LocationDescription " +
                                      "where LocationId = @LocationId";
                    cmd.Parameters.AddWithValue("LocationId", location.LocationId);
                    cmd.Parameters.AddWithValue("Hits", location.Hits);
                    cmd.Parameters.AddWithValue("IsTopLocation", location.IsTopLocation);
                    cmd.Parameters.AddWithValue("LocationName", location.LocationName);
                    cmd.Parameters.AddWithValue("Latitude", location.Latitude);
                    cmd.Parameters.AddWithValue("Longitude", location.Longitude);
                    cmd.Parameters.AddWithValue("UserId", location.User.UserId);
                    cmd.Parameters.AddWithValue("LocationDescription", location.LocationDescription);
                    cmd.ExecuteNonQuery();
                }
                _connection.Close();
            }
        }

        public void UserUpdateLocation(Location location)
        {
            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Locations set LocationName = @LocationName, LocationDescription = @LocationDescription " +
                                      "where LocationId = @LocationId";
                    cmd.Parameters.AddWithValue("LocationId", location.LocationId);
                    cmd.Parameters.AddWithValue("LocationName", location.LocationName);
                    cmd.Parameters.AddWithValue("LocationDescription", location.LocationDescription);
                    cmd.ExecuteNonQuery();
                }
                _connection.Close();
            }
        }

        public void UpdateHits(Location location)
        {
            for (int i = 0; i < 5; i++)
            {
                using (_connection = new SqlConnection(_connectionString))
                {
                    try
                    {
                        _connection.Open();
                        using (SqlTransaction trans = _connection.BeginTransaction())
                        {
                            byte[] rowVersion = null;
                            int rowCount = 0;
                            using (SqlCommand cmd = _connection.CreateCommand())
                            {
                                cmd.Transaction = trans;
                                cmd.CommandText = "SELECT [rowVersion] FROM Locations WHERE LocationId = @LocationId";
                                cmd.Parameters.AddWithValue("LocationId", location.LocationId);
                                SqlDataReader reader = cmd.ExecuteReader();

                                while (reader.Read())
                                {
                                    rowVersion = (byte[]) reader.GetValue(reader.GetOrdinal("rowVersion"));
                                }

                                reader.Close();

                                cmd.CommandText =
                                    "UPDATE Locations set Hits = Hits + 1 where LocationId = @LocationId AND rowVersion = @RowVersion";
                                cmd.Parameters.AddWithValue("RowVersion", rowVersion);
                                rowCount = cmd.ExecuteNonQuery();

                                if (rowCount == 0)
                                {
                                    cmd.Transaction.Rollback();
                                }
                                else
                                {
                                    cmd.Transaction.Commit();
                                    break;
                                }
                            }
                        }
                        _connection.Close();
                    }

                    catch (SqlException e)
                    {
                        Console.Write(e.StackTrace);
                    }
                }
            }
        }

        public void Delete(int locationId)
        {
            int changes;

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Locations where LocationId = @LocationId";
                    cmd.Parameters.AddWithValue("LocationId", locationId);
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
