using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MobileService.Exception;

namespace MobileService.Database
{
    public class DbConnection
    {
        private static SqlConnection _connection;
        private readonly string _connectionString = "Server=kraka.ucn.dk;" +
                                                    "Database=dmaa0917_1067347;User ID=dmaa0917_1067347;" +
                                                    "Password=Password1!;";
        
        /// <summary>
        /// Tests the connection to the database
        /// </summary>
        public void ConnectionTest()
        {
            try
            {
                using (_connection = new SqlConnection(_connectionString))
                {
                    _connection.Open();

                    using (SqlCommand cmd = _connection.CreateCommand())
                    {
                        cmd.CommandText = "SELECT 1";
                        int idStatus = Convert.ToInt32(cmd.ExecuteScalar());
                        if (idStatus != 1)
                        {
                            throw new FaultException<Exception.Exception>(new Exception.Exception());
                        }
                    }
                   _connection.Close();
                }
            }
            catch (SqlException e)
            {
                throw new FaultException<Exception.Exception>(new Exception.Exception());
            }
        }
    }


}
