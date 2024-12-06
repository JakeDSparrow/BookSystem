using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSystem
{
    public class SqlConnectionClass
    {
        private string connection = "Data Source=DESKTOP-814NNKN;Initial Catalog=BookSystemDB;Integrated Security=True;Encrypt=False";

        public string GetConnectionString()
        {
            return connection;
        }

        // Method to create a SqlConnection object
        public SqlConnection GetConnection()
        {
            return new SqlConnection(connection);
        }
    }
}
