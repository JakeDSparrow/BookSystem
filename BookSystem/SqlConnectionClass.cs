﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSystem
{
    public class SqlConnectionClass
    {
        private string connection = "Data Source=DESKTOP-814NNKN;Initial Catalog=BookSystem;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";

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
