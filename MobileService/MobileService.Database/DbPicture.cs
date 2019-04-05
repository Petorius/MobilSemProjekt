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
    public class DbPicture
    {   
        public int Create(Picture picture, int locationId)
        {
            DbConnection dbc = new DbConnection();
            SqlConnection sqlC = dbc.Connection;
            int id;

            using (sqlC)
            {
                sqlC.Open();

                using (SqlCommand cmd = sqlC.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Picture(URL, PictureName, Description, LocationId) VALUES " +
                                      "(@URL, @PictureName, @Description, @LocationId); ";
                    cmd.Parameters.AddWithValue("URL", picture.Url);
                    cmd.Parameters.AddWithValue("PictureName", picture.PictureName);
                    cmd.Parameters.AddWithValue("Description", picture.Description);

                    if (locationId != 0)
                    {
                        cmd.Parameters.AddWithValue("LocationId", locationId);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("LocationId", DBNull.Value);
                    }
                    
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return id;
        }
        public Picture FindById(int pictureId)
        {
            DbConnection dbc = new DbConnection();
            SqlConnection sqlC = dbc.Connection;
            Picture picture = null;

            using (sqlC)
            {
                sqlC.Open();
                using (SqlCommand cmd = sqlC.CreateCommand())
                {

                    cmd.CommandText = "SELECT * FROM Picture WHERE PictureId = @PictureId";
                    cmd.Parameters.AddWithValue("PictureId", pictureId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        picture = new Picture
                        {
                            PictureId = pictureId,
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            PictureName = reader.GetString(reader.GetOrdinal("PictureName")),
                            Url = reader.GetString(reader.GetOrdinal("Url"))
                        };
                    }
                }
            }
            return picture;
        }
        public List<Picture> FindByLocationId(int locationId)
        {
            DbConnection dbc = new DbConnection();
            SqlConnection sqlC = dbc.Connection;
            List<Picture> pictures = new List<Picture>();

            using (sqlC)
            {
                sqlC.Open();
                using (SqlCommand cmd = sqlC.CreateCommand())
                {   
                    cmd.CommandText = "SELECT * FROM Picture WHERE LocationId = @LocationId";
                    cmd.Parameters.AddWithValue("LocationId", locationId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Picture picture = new Picture
                        {
                            PictureId = reader.GetInt32(reader.GetOrdinal("PictureId")),
                            Description = reader.GetString(reader.GetOrdinal("Description")),
                            PictureName = reader.GetString(reader.GetOrdinal("PictureName")),
                            Url = reader.GetString(reader.GetOrdinal("Url"))
                        };
                        pictures.Add(picture);
                    }
                }
            }
            return pictures;
        }

        /*
         * TOBE Updated
         */
        public void Update(Picture picture, int locationId, int pictureId)
        {
            DbConnection dbc = new DbConnection();
            SqlConnection sqlC = dbc.Connection;

            using (sqlC)
            {
                sqlC.Open();
                using (SqlCommand cmd = sqlC.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Picture set URL = @URL, " +
                                      "PictureName = @PictureName, Description = @Description, LocationId = @LocationId " +
                                      "where PictureId = @PictureId";
                    cmd.Parameters.AddWithValue("PictureId", pictureId);
                    cmd.Parameters.AddWithValue("URL", picture.Url);
                    cmd.Parameters.AddWithValue("PictureName", picture.PictureName);
                    cmd.Parameters.AddWithValue("Description", picture.Description);
                    cmd.Parameters.AddWithValue("LocationId", locationId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int pictureId)
        {
            DbConnection dbc = new DbConnection();
            SqlConnection sqlC = dbc.Connection;
            int changes;

            using (sqlC)
            {
                sqlC.Open();
                using (SqlCommand cmd = sqlC.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Picture where PictureId = @PictureId";
                    cmd.Parameters.AddWithValue("PictureId", pictureId);
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
