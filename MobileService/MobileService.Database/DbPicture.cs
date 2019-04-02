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
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DBString"].ConnectionString;

        public int Create(Picture picture, int locationId)
        {
            int id;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = connection.CreateCommand())
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
                        cmd.Parameters.AddWithValue("locationId", DBNull.Value);
                    }
                    
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return id;
        }
        public Picture FindById(int pictureId)
        {
            Picture picture = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
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
            List<Picture> pictures = new List<Picture>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = connection.CreateCommand())
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
    }
}
