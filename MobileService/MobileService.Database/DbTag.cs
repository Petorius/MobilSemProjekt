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
    public class DbTag
    {
        private readonly DbLocation _dbLocation;
        private static SqlConnection _connection;
        private readonly string _connectionString = "Server=kraka.ucn.dk;" +
                                                    "Database=dmaa0917_1067347;User ID=dmaa0917_1067347;" +
                                                    "Password=Password1!;";

        public DbTag()
        {
            _dbLocation = new DbLocation();
        }

        public int Create(Tag tag)
        {
            int id;

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();

                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Tag(TagName) VALUES (@TagName); ";
                    cmd.Parameters.AddWithValue("TagName", tag.TagName);
                    
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
                _connection.Close();
            }
            return id;
        }

        public Tag FindById(int tagId)
        {
            Tag tag = null;

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Tag WHERE TagId = @TagId";
                    cmd.Parameters.AddWithValue("TagId", tagId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        tag = new Tag
                        {
                            TagId = tagId,
                            Locations = GetLocationsByTagId(tagId),
                            TagName = reader.GetString(reader.GetOrdinal("TagName"))
                        };
                    }
                }
                _connection.Close();
            }
            return tag;
        }

        public Tag FindByName(string tagName)
        {
            Tag tag = null;

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Tag WHERE TagName = @TagName";
                    cmd.Parameters.AddWithValue("TagName", tagName);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int tagId = reader.GetInt32(reader.GetOrdinal("TagId"));
                        
                        tag = new Tag
                        {
                            TagId = tagId,
                            Locations = GetLocationsByTagId(tagId),
                            TagName = tagName
                        };
                    }
                }
                _connection.Close();
            }
            return tag;
        }


        public List<Location> GetLocationsByTagId(int tagId)
        {
            List<Location> locations = new List<Location>();

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT locationId FROM LocationTag WHERE TagId = @TagId";
                    cmd.Parameters.AddWithValue("TagId", tagId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int locationId = reader.GetInt32(reader.GetOrdinal("locationId"));
                        Location location = _dbLocation.FindById(locationId);
                        locations.Add(location);
                    }
                }
                _connection.Close();
            }
            return locations;
        }

        public bool Update(Tag tag)
        {
            int changes;

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Tag SET TagName = @TagName WHERE TagId = @TagId";
                    cmd.Parameters.AddWithValue("TagId", tag.TagId);
                    cmd.Parameters.AddWithValue("TagName", tag.TagName);
                    changes = cmd.ExecuteNonQuery();
                }
                _connection.Close();
            }

            return changes > 0;
        }

        public void Delete(int tagId)
        {
            int changes;

            using (_connection = new SqlConnection(_connectionString))
            {
                _connection.Open();
                using (SqlCommand cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Tag where TagId = @TagId";
                    cmd.Parameters.AddWithValue("TagId", tagId);
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
