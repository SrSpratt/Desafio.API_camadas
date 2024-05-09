using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desafio.Infrastructure.Connections;
using Microsoft.Extensions.Configuration;

namespace Testes.Context
{
    public class ConnectionTest
    {
        private readonly ConnectionManager _connectionManager;
        private readonly IConfiguration _configuration; 

        public ConnectionTest(IConfiguration configuration)
        {
            _configuration = configuration;
            string connectionString = _configuration["ApiConfig:ConnectionStrings:DefaultConnection"];
            _connectionManager = new ConnectionManager(connectionString);
        }

        public void Execute()
        {
            ValidateConnectivity();
        }

        private void ValidateConnectivity()
        {
            SqlConnection connection = null;
            try
            {
                connection = _connectionManager.GetConnection();
                connection.Open();

                string sql_select = "select * from tst_products";

                
                SqlCommand cmd = new SqlCommand(sql_select, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string? first = reader[1].ToString();
                        string second = reader.GetString("Supplier");
                        int third = reader.GetInt32("Code");
                    }
                }

                
                cmd = null;
                reader.Close();
                reader = null;


            } catch (Exception ex)
            {
                throw ex;
            } finally
            {
                if ((connection.State == System.Data.ConnectionState.Open))
                {
                    connection.Close();
                    connection = null;
                }
                
            }
        }
    }
}
