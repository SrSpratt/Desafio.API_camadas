using Desafio.Domain.Entities;
using Desafio.Domain.Enums;
using Desafio.Domain.Setup;
using Desafio.Infrastructure.Connections;
using Desafio.Infrastructure.Queries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Contexts
{
    public class SqlContext : IContext
    {
        private readonly IApiConfig _apiConfig;
        private readonly ConnectionManager _connectionManager;
        private static string _connectionString = "";

        public SqlContext(IApiConfig apiConfig)
        {
            _apiConfig = apiConfig;
            _connectionString = _apiConfig.ConnectionStrings.DefaultConnection;
            _connectionManager = new ConnectionManager(_connectionString);
        }

        public void Add(Product product)
        {
            SqlConnection sqlConnection = null;

            try
            {
                string sql = SqlManager.GetSql(SqlQueryType.CREATE);

                sqlConnection = _connectionManager.GetConnection();
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = product.Name;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = product.Description;
                cmd.Parameters.Add("@SaleValue", SqlDbType.Real).Value = product.SaleValue;
                cmd.Parameters.Add("@Supplier", SqlDbType.VarChar).Value = product.Supplier;
                cmd.Parameters.Add("@Value", SqlDbType.Real).Value = product.Value;
                cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = product.Category;
                cmd.Parameters.Add("@ExpirationDate", SqlDbType.VarChar).Value = product.ExpirationDate;
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                cmd = null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
                sqlConnection = null;
            }
        }

        public void Delete(int id)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = _connectionManager.GetConnection();
                string sql = SqlManager.GetSql(SqlQueryType.DELETE);
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.Parameters.Add("@Code", SqlDbType.Int).Value = id;
                sqlConnection.Open();
                cmd.ExecuteNonQuery();

                cmd = null;

            }
            catch (Exception ex)
            {

                throw ex;
            } finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
                sqlConnection = null;
            }
        }

        public Product Get(int id)
        {
            Product result = null;
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = _connectionManager.GetConnection();
                string sql = SqlManager.GetSql(SqlQueryType.READ);

                DataSet set = new DataSet();

                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.Parameters.Add("@Code", SqlDbType.Int).Value = id;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(set, "queryResult");

                foreach(DataRow row in set.Tables["queryResult"].Rows)
                {
                    result = new Product(
                        code: Int32.Parse(row["Code"].ToString()),
                        description: row["Description"].ToString(),
                        SaleValue: Double.Parse(row["SaleValue"].ToString()),
                        name: row["Name"].ToString(),
                        supplier: row["Supplier"].ToString(),
                        value: Double.Parse(row["Value"].ToString()),
                        category: row["Category"].ToString(),
                        expirationDate: row["ExpirationDate"].ToString()
                        );
                }
                return result;

                set.Clear();
                set = null;
                cmd = null;
            }
            catch (Exception ex)
            {

                throw ex;
            } finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
                sqlConnection = null;
            }
        }

        public List<Product> GetAll()
        {

            List<Product> list = new List<Product>();
            SqlConnection sqlConnection = null;
            try
            {
                string sql = SqlManager.GetSql(SqlQueryType.READALL);

                DataSet set = new DataSet();
                sqlConnection = _connectionManager.GetConnection();
                //sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(set, "queryResult");
                foreach(DataRow row in set.Tables["queryresult"].Rows)
                {
                    Product product = new Product(
                        code: Int32.Parse(row["Code"].ToString()),
                        description: row["Description"].ToString(),
                        SaleValue: Double.Parse(row["SaleValue"].ToString()),
                        name: row["Name"].ToString(),
                        supplier: row["Supplier"].ToString(),
                        value: Double.Parse(row["Value"].ToString()),
                        category: row["Category"].ToString(),
                        expirationDate: row["ExpirationDate"].ToString()
                        );
                    list.Add(product);
                }
                return list;
            } catch (Exception ex) 
            {
                throw ex;
            }
            finally
            {
                if(sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
                sqlConnection = null;
            }
            
        }

        public int NextId()
        {
            return 0;
        }

        public void Update(int id, Product product)
        {
            Product result = null;
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = _connectionManager.GetConnection();
                string sql = SqlManager.GetSql(SqlQueryType.UPDATE);

                DataSet set = new DataSet();

                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.Parameters.Add("@Code", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = product.Name;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = product.Description;
                cmd.Parameters.Add("@SaleValue", SqlDbType.Real).Value = product.SaleValue;
                cmd.Parameters.Add("@Supplier", SqlDbType.VarChar).Value = product.Supplier;
                cmd.Parameters.Add("@Value", SqlDbType.Real).Value = product.Value;
                cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = product.Category;
                cmd.Parameters.Add("@ExpirationDate", SqlDbType.VarChar).Value = product.ExpirationDate;
                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                cmd = null;
                /*
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(set, "queryResult");
                
                foreach (DataRow row in set.Tables["queryResult"].Rows)
                {
                    result = new Product(
                        code: Int32.Parse(row["Code"].ToString()),
                        description: row["Description"].ToString(),
                        SaleValue: Double.Parse(row["SaleValue"].ToString()),
                        name: row["Name"].ToString(),
                        supplier: row["Supplier"].ToString(),
                        value: Double.Parse(row["Value"].ToString()),
                        category: row["Category"].ToString(),
                        expirationDate: row["ExpirationDate"].ToString()
                        );
                }
                return result;*/

                set.Clear();
                set = null;
                cmd = null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
                sqlConnection = null;
            }

        }
    }
}
