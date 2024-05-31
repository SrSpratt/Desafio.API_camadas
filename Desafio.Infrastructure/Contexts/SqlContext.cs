using Desafio.Domain.Daos;
using Desafio.Domain.Dtos;
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

        public async Task<int> Add(Product product)
        {
            SqlConnection sqlConnection = null;

            try
            {
                string sql = SqlManager.GetSql(SqlQueryType.NEWCREATE);

                sqlConnection = _connectionManager.GetConnection();
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = product.Name;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = product.Description;
                cmd.Parameters.Add("@SaleValue", SqlDbType.Real).Value = product.SaleValue;
                cmd.Parameters.Add("@Supplier", SqlDbType.VarChar).Value = product.Supplier;
                cmd.Parameters.Add("@Value", SqlDbType.Real).Value = product.Value;
                cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = product.Category;
                cmd.Parameters.Add("@ExpirationDate", SqlDbType.DateTime).Value = product.ExpirationDate;
                cmd.Parameters.Add("@Amount", SqlDbType.Int).Value = product.Amount;
                
                return await Task.Run(
                    () => {
                        sqlConnection.Open();
                        int id = (int) cmd.ExecuteScalar();

                        cmd = null;
                        return id;
                    });

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

        public async Task Delete(int id)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = _connectionManager.GetConnection();
                string sql = SqlManager.GetSql(SqlQueryType.NEWDELETE);
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.Parameters.Add("@Code", SqlDbType.Int).Value = id;

                await Task.Run(
                    () => {
                        sqlConnection.Open();
                        cmd.ExecuteNonQuery();

                        cmd = null;
                    });

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

        public async Task<UserDTO> Login(string username)
        {
            SqlConnection sqlConnection = null;
            UserDTO user = null;
            try
            {
                sqlConnection = _connectionManager.GetConnection();
                string sql = SqlManager.GetSql(SqlQueryType.READUSER);
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                await sqlConnection.OpenAsync();
                var row = await cmd.ExecuteReaderAsync();
                if (await row.ReadAsync())
                {
                    for (int i = 0; i < 6; i++)
                    {
                        var a = row.GetValue(i);
                    }
                    user = new UserDTO(
                            new UserDAO
                            {
                                ID = row.GetInt32(0),
                                Name = row.GetString(1),
                                Password = row.GetString(3),
                                Email = row.GetString(2),
                            },
                            new RoleDAO
                            {
                                ID = row.GetInt32(6),
                                Type = row.GetString(5)
                            }
                        );
                }
            } catch (Exception ex)
            {
                throw ex;
            } finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
                sqlConnection = null;
            }
            return user;
        }


        public async Task<string> GetCategory(int id) //método refatorado
        {
            string result = "";
            SqlConnection sqlConnection = null;

            try
            {
                sqlConnection = _connectionManager.GetConnection();
                string sql = SqlManager.GetSql(SqlQueryType.READNAME);

                DataSet set = new DataSet();

                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.Parameters.Add("@Code", SqlDbType.VarChar).Value = id;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                return await Task<string>.Run(
                    () => {
                        adapter.Fill(set, "queryResult");

                        foreach (DataRow row in set.Tables["queryResult"].Rows)
                        {
                            result = row["Category"].ToString();
                        }

                        set.Clear();
                        set = null;
                        cmd = null;
                        return result!;
                    });


            } catch (Exception ex)
            {
                throw ex;
            } finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
                sqlConnection = null;
            }

        }

        public async Task<Product> Get(int id)
        {
            Product result = null;
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = _connectionManager.GetConnection();
                string sql = SqlManager.GetSql(SqlQueryType.NEWREAD);

                DataSet set = new DataSet();

                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.Parameters.Add("@Code", SqlDbType.Int).Value = id;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);


                return await Task.Run(
                    () => 
                    {
                        dataAdapter.Fill(set, "queryResult");

                        foreach (DataRow row in set.Tables["queryresult"].Rows)
                        {
                            StockDao stockRep = new StockDao(
                                amount: Int32.Parse(row["Amount"].ToString()),
                                saleValue: Double.Parse(row["Value"].ToString()),
                                supplier: row["Supplier"].ToString(),
                                purchaseValue: Double.Parse(row["Purchase Value"].ToString()),
                                expirationDate: !string.IsNullOrEmpty(row["Expiration Date"].ToString()) ? DateTime.Parse(row["Expiration Date"].ToString()) : DateTime.MinValue
                                );
                            ProductDao productRep = new ProductDao(
                                code: Int32.Parse(row["Code"].ToString()),
                                name: row["Name"].ToString(),
                                description: row["Description"].ToString()
                                );
                            CategoryDao categoryRep = new CategoryDao(
                                name: row["Category"].ToString(),
                                description: row["Description"].ToString()
                                );

                            result = new Product(
                                categoryInfo: categoryRep,
                                productInfo: productRep,
                                stockInfo: stockRep
                                );
                        }
                        return result!;
                    });

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

        public async Task<List<Product>> GetAll()
        {

            List<Product> list = new List<Product>();
            SqlConnection sqlConnection = null;
            try
            { 
                string sql = SqlManager.GetSql(SqlQueryType.NEWREADALL);

                DataSet set = new DataSet();
                sqlConnection = _connectionManager.GetConnection();
                //sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                return await Task.Run(
                    () =>
                    {
                        adapter.Fill(set, "queryResult");
                        foreach (DataRow row in set.Tables["queryresult"].Rows)
                        {
                            StockDao stockRep = new StockDao(
                                amount: Int32.Parse(row["Amount"].ToString()),
                                saleValue: Double.Parse(row["Value"].ToString()),
                                supplier: row["Supplier"].ToString(),
                                purchaseValue: Double.Parse(row["Purchase Value"].ToString()),
                                expirationDate: !string.IsNullOrEmpty(row["Expiration Date"].ToString()) ? DateTime.Parse(row["Expiration Date"].ToString()) : DateTime.MinValue
                                );
                            ProductDao productRep = new ProductDao(
                                code: Int32.Parse(row["Code"].ToString()),
                                name: row["Name"].ToString(),
                                description: row["Description"].ToString()
                                );
                            CategoryDao categoryRep = new CategoryDao(
                                name: row["Category"].ToString(),
                                description: row["Description"].ToString()
                                );

                            Product product = new Product(
                                categoryInfo: categoryRep,
                                productInfo: productRep,
                                stockInfo: stockRep
                                );
                            list.Add(product);
                        }
                        return list;
                    });
                
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

        public async Task Update(int id, Product product)
        {

            string category = await GetCategory(id);
            Product result = null;
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = _connectionManager.GetConnection();
                string sql = SqlManager.GetSql(SqlQueryType.NEWUPDATE);

                DataSet set = new DataSet();

                await Task.Run(
                    () =>
                    {
                        SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                        cmd.Parameters.Add("@Code", SqlDbType.Int).Value = id;
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = product.Name;
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = product.Description;
                        cmd.Parameters.Add("@SaleValue", SqlDbType.Real).Value = product.SaleValue;
                        cmd.Parameters.Add("@Supplier", SqlDbType.VarChar).Value = product.Supplier;
                        cmd.Parameters.Add("@Value", SqlDbType.Real).Value = product.Value;
                        cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = product.Category;
                        cmd.Parameters.Add("@OldCategory", SqlDbType.VarChar).Value = product.Category;
                        cmd.Parameters.Add("@ExpirationDate", SqlDbType.DateTime).Value = product.ExpirationDate;
                        cmd.Parameters.Add("@Amount", SqlDbType.Int).Value = product.Amount;
                        sqlConnection.Open();
                        cmd.ExecuteNonQuery();
                        cmd = null;
                    });


                set.Clear();
                set = null;
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
