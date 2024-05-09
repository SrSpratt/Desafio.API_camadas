using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Connections
{
    public class ConnectionManager
    {
        private static string _ConnectionString = "";
        private static SqlConnection connection = null;

        public ConnectionManager(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {

            if (connection == null)
            {
                connection = new SqlConnection(_ConnectionString);
            }
            return connection;
        }
    }
}
