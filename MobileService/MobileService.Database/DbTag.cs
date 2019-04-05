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

        public DbTag()
        {
            _dbLocation = new DbLocation();
        }

        public int Create(Tag tag)
        {
            DbConnection dbc = new DbConnection();
            SqlConnection sqlC = dbc.Connection;
            int id;

            using (sqlC)
            {
                sqlC.Open();

                using (SqlCommand cmd = sqlC.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Tag(TagName) VALUES " +
                                      "(@TagName); ";
                    cmd.Parameters.AddWithValue("TagName", tag.TagName);
                    
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return id;
        }

        public Tag FindById(int tagId)
        {
            DbConnection dbc = new DbConnection();
            SqlConnection sqlC = dbc.Connection;
            Tag tag = null;

            using (sqlC)
            {
                sqlC.Open();
                using (SqlCommand cmd = sqlC.CreateCommand())
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
            }
            return tag;
        }

        public Tag FindByName(string tagName)
        {
            DbConnection dbc = new DbConnection();
            SqlConnection sqlC = dbc.Connection;
            Tag tag = null;

            using (sqlC)
            {
                sqlC.Open();
                using (SqlCommand cmd = sqlC.CreateCommand())
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
            }
            return tag;
        }


        public List<Location> GetLocationsByTagId(int tagId)
        {
            DbConnection dbc = new DbConnection();
            SqlConnection sqlC = dbc.Connection;
            List<Location> locations = new List<Location>();

            using (sqlC)
            {
                sqlC.Open();
                using (SqlCommand cmd = sqlC.CreateCommand())
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
            }
            return locations;
        }

        public void Delete(int tagId)
        {
            DbConnection dbc = new DbConnection();
            SqlConnection sqlC = dbc.Connection;
            int changes;

            using (sqlC)
            {
                sqlC.Open();
                using (SqlCommand cmd = sqlC.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Tag where TagId = @TagId";
                    cmd.Parameters.AddWithValue("TagId", tagId);
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
