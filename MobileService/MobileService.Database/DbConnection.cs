using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MobileService.Database
{
    public class DbConnection
    {
        private readonly string _connectionString = "Server=kraka.ucn.dk;Database=dmaa0917_1067347;User ID=dmaa0917_1067347;Password=Password1!;";
        private SqlConnection connection;

        public SqlConnection Connection
        {
            get { return connection; }
        }

        public DbConnection()
        {
            connection = new SqlConnection(_connectionString);
        }

        public bool ConnectionTest()
        {
            bool status = true;
            try
            {
                using (connection = new SqlConnection(_connectionString))
                {

                    connection.Open();

                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT 1";
                        int idStatus = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idStatus != 1)
                        {
                            Debug.WriteLine("Db error " + idStatus);
                            status = false; 
                            //throw new FaultException<DbConnectionError>(new DbConnectionError());
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                status = false;
                Debug.WriteLine("SQLError " + e.Message);

                //throw new FaultException<DbConnectionError>(new DbConnectionError());
            }

            return status;
        }
    }


}
