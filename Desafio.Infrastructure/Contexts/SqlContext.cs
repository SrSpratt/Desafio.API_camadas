using Desafio.Domain.Daos;
using Desafio.Domain.Dtos;
using Desafio.Domain.Enums;
using Desafio.Domain.Setup;
using Desafio.Infrastructure.Connections;
using Desafio.Infrastructure.Queries;
using System.Data;
using System.Data.SqlClient;
using System.Text;

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


        #region //Products
        public async Task<int> Create(ProductDTO product)
        {
            SqlConnection sqlConnection = null;

            try
            {
                string sql = SqlManager.GetSql(SqlQueryType.CREATE);

                sqlConnection = _connectionManager.GetConnection();
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = product.Name;
                cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = product.Description;
                cmd.Parameters.Add("@saleValue", SqlDbType.Real).Value = product.SaleValue;
                cmd.Parameters.Add("@supplier", SqlDbType.VarChar).Value = product.Supplier;
                cmd.Parameters.Add("@purchasevalue", SqlDbType.Real).Value = product.Value;
                cmd.Parameters.Add("@category", SqlDbType.VarChar).Value = product.Category;
                cmd.Parameters.Add("@expirationdate", SqlDbType.DateTime).Value = product.ExpirationDate;
                cmd.Parameters.Add("@amount", SqlDbType.Int).Value = product.Amount;
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = product.Operation.OperationUser;
                cmd.Parameters.Add("@operation", SqlDbType.VarChar).Value = product.Operation.OperationType;
                cmd.Parameters.Add("@operationamount", SqlDbType.Int).Value = product.Operation.OperationAmount;
                //string[] newsql = sql.Split(";");
                return await Task.Run(
                    () => {
                        sqlConnection.Open();
                        int id = (int)cmd.ExecuteScalar();

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
                string sql = SqlManager.GetSql(SqlQueryType.DELETE);
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

        public async Task<ProductDTO> Get(int id)
        {
            ProductDTO result = null;
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = _connectionManager.GetConnection();
                string sql = SqlManager.GetSql(SqlQueryType.READ);

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
                            StockDAO stockRep = new StockDAO(
                                amount: Int32.Parse(row["Amount"].ToString()),
                                saleValue: Double.Parse(row["Value"].ToString()),
                                supplier: row["Supplier"].ToString(),
                                purchaseValue: Double.Parse(row["Purchase Value"].ToString()),
                                expirationDate: !string.IsNullOrEmpty(row["Expiration Date"].ToString()) ? DateTime.Parse(row["Expiration Date"].ToString()) : DateTime.MinValue
                                );
                            ProductDAO productRep = new ProductDAO(
                                code: Int32.Parse(row["Code"].ToString()),
                                name: row["Name"].ToString(),
                                description: row["Description"].ToString()
                                );
                            CategoryDAO categoryRep = new CategoryDAO(
                                name: row["Category"].ToString(),
                                description: row["Description"].ToString()
                                );

                            result = new ProductDTO(
                                categoryInfo: categoryRep,
                                productInfo: productRep,
                                stockInfo: stockRep
                                );
                        }

                        return result;
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

        public async Task<List<ProductDTO>> GetAll()
        {

            List<ProductDTO> list = new List<ProductDTO>();
            SqlConnection sqlConnection = null;
            try
            {
                string sql = SqlManager.GetSql(SqlQueryType.READALL);

                DataSet set = new DataSet();
                sqlConnection = _connectionManager.GetConnection();
                //sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                return await Task.Run(
                    async () =>
                    {
                        adapter.Fill(set, "queryResult");
                        foreach (DataRow row in set.Tables["queryresult"].Rows)
                        {
                            StockDAO stockRep = new StockDAO(
                                amount: Int32.Parse(row["Amount"].ToString()),
                                saleValue: Double.Parse(row["Value"].ToString()),
                                supplier: row["Supplier"].ToString(),
                                purchaseValue: Double.Parse(row["Purchase Value"].ToString()),
                                expirationDate: !string.IsNullOrEmpty(row["Expiration Date"].ToString()) ? DateTime.Parse(row["Expiration Date"].ToString()) : DateTime.MinValue
                                );
                            ProductDAO productRep = new ProductDAO(
                                code: Int32.Parse(row["Code"].ToString()),
                                name: row["Name"].ToString(),
                                description: row["Description"].ToString()
                                );
                            CategoryDAO categoryRep = new CategoryDAO(
                                name: row["Category"].ToString(),
                                description: row["Description"].ToString()
                                );

                            ProductDTO product = new ProductDTO(
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
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
                sqlConnection = null;
            }

        }

        public async Task Update(int id, ProductDTO product)
        {

            string category = await GetCategory(id);
            ProductDTO result = null;
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = _connectionManager.GetConnection();
                string sql = SqlManager.GetSql(SqlQueryType.UPDATE);

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
                        var affectedRows = cmd.ExecuteNonQuery();
                        cmd = null;

                        if (affectedRows < 1)
                        {
                            throw new ArgumentException("Update not made");
                        }
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

        #endregion 

        public async Task<List<OperationDTO>> GetAllOperations(int id)
        {
            string sql = SqlManager.GetSql(SqlQueryType.READOPERATIONS);
            List<OperationDTO> operations = new List<OperationDTO>();
            SqlConnection sqlConnection = null;

            try
            {
                sqlConnection = _connectionManager.GetConnection();
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);

                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                await sqlConnection.OpenAsync();
                var row = await cmd.ExecuteReaderAsync();
                while (await row.ReadAsync())
                {
                    operations.Add(
                        new OperationDTO(
                            new OperationDAO()
                            {
                                OperationId = row.GetInt32(row.GetOrdinal("operation_id")),
                                OperationAmount = row.GetInt32(row.GetOrdinal("operation_amount")),
                                OperationType = row.GetString(row.GetOrdinal("operation_type")),
                                OperationDate = row.GetDateTime(row.GetOrdinal("operation_date")),
                                OperationUser = row.GetString(row.GetOrdinal("operation_user")),
                                StockId = row.GetInt32(row.GetOrdinal("stock_id"))
                            }
                            )
                        );
                }

                return operations;
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

        #region //Users
        public async Task<List<UserDTO>> GetAllUsers()
        {
            SqlConnection sqlConnection = null;
            List<UserDTO> userDTOs = new List<UserDTO>();

            try
            {
                sqlConnection = _connectionManager.GetConnection();
                string sql = SqlManager.GetSql(SqlQueryType.READUSERS);
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                await sqlConnection.OpenAsync();
                var row = await cmd.ExecuteReaderAsync();
                while (await row.ReadAsync())
                {
                    userDTOs.Add(
                        new UserDTO(
                            new UserDAO
                            {
                                ID = row.GetInt32(row.GetOrdinal("user_id")),
                                Name = row.GetString(row.GetOrdinal("username")),
                                Password = row.GetString(row.GetOrdinal("user_password")),
                                Email = row.GetString(row.GetOrdinal("user_email")),
                                DateRegistered = row.GetDateTime(row.GetOrdinal("date_registered")),
                                UserRegistered = row.GetString(row.GetOrdinal("user_registered")),
                                RealName = row.GetString(row.GetOrdinal("concatenated_user_names"))
                            },
                            new RoleDAO
                            {
                                ID = row.GetInt32("role_id"),
                                Type = row.GetString("role_type")
                            }
                            )
                        );
                }
                return userDTOs;
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


        public async Task DeleteUser(int id)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = _connectionManager.GetConnection();
                string sql = SqlManager.GetSql(SqlQueryType.DELETEUSER);
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                await sqlConnection.OpenAsync();
                int numberrows = await cmd.ExecuteNonQueryAsync();

                if (numberrows < 1)
                    throw new ArgumentException("Delete not working!");

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

        public async Task<UserDTO> GetUser(int id)
        {
            SqlConnection sqlConnection = null;
            UserDTO user = null;
            try
            {
                sqlConnection = _connectionManager.GetConnection();
                string sql = SqlManager.GetSql(SqlQueryType.READUSER);
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = id;
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
                                ID = row.GetInt32(row.GetOrdinal("user_id")),
                                Name = row.GetString(row.GetOrdinal("username")),
                                Password = row.GetString(row.GetOrdinal("user_password")),
                                Email = row.GetString(row.GetOrdinal("user_email")),
                                DateRegistered = row.GetDateTime(row.GetOrdinal("date_registered")),
                                UserRegistered = row.GetString(row.GetOrdinal("user_registered")),
                                RealName = row.GetString(row.GetOrdinal("concatenated_user_names"))
                            },
                            new RoleDAO
                            {
                                ID = row.GetInt32("role_id"),
                                Type = row.GetString("role_type")
                            }
                        );
                }
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
            return user;
        }

        public async Task<UserDTO> Login(string name) //GetUserByName
        {
            SqlConnection sqlConnection = null;
            UserDTO user = null;
            try
            {
                sqlConnection = _connectionManager.GetConnection();
                string sql = SqlManager.GetSql(SqlQueryType.READUSERNAME);
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                await sqlConnection.OpenAsync();
                var row = await cmd.ExecuteReaderAsync();
                if (await row.ReadAsync())
                {
                    user = new UserDTO(
                        new UserDAO
                        {
                            Name = row.GetString(row.GetOrdinal("username")),
                            ID = row.GetInt32(row.GetOrdinal("user_id")),
                            Password = row.GetString(row.GetOrdinal("user_password")),
                            Email = row.GetString(row.GetOrdinal("user_email")),
                        },
                        new RoleDAO
                        {
                            Type = row.GetString(row.GetOrdinal("role_type")),
                            ID = row.GetInt32(row.GetOrdinal("user_id"))
                        }
                        );
                }

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
            return user;
        }

        public async Task<int> GetRole(string rolename)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = _connectionManager.GetConnection();
                string sql = SqlManager.GetSql(SqlQueryType.READROLE);
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                await sqlConnection.OpenAsync();
                cmd.Parameters.Add("@role", SqlDbType.VarChar).Value = rolename;
                var row = await cmd.ExecuteReaderAsync();
                int id = 0;
                if (await row.ReadAsync())
                {
                    id = row.GetInt32(0);
                }
                return id;

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

        public async Task<int> CreateUser(UserDTO user)
        {
            
            SqlConnection sqlConnection = null;
            int role = await GetRole(user.Role);
            if (role == 0)
                throw new Exception("Role does not exist");
            try
            {
                sqlConnection = _connectionManager.GetConnection();
                string sql = SqlManager.GetSql(SqlQueryType.CREATEUSER);
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = user.Name;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = user.Email;
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = user.Password;
                cmd.Parameters.Add("@role", SqlDbType.Int).Value = role;
                cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user.UserRegistered;
                sqlConnection.OpenAsync();
                var id = (int)await cmd.ExecuteScalarAsync();

                sql = null;
                sql = SqlManager.GetSql(SqlQueryType.INSERTNAMEUSER);
                string[] names = user.RealName.Split(" ");
                foreach (string name in names)
                {
                    cmd = null;
                    cmd = new SqlCommand(sql, sqlConnection);
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    cmd.Parameters.Add("@realname", SqlDbType.VarChar).Value = name;
                    var rows = await cmd.ExecuteNonQueryAsync();
                    if (rows < 1)
                    {
                        throw new ArgumentException("Create not working!");
                    }
                }

                return id;
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

        public async Task UpdateUser(int id, UserDTO user)
        {
            int role = await GetRole(user.Role);
            SqlConnection sqlConnection = null;
            UserDTO result = null;
            try
            {
                sqlConnection = _connectionManager.GetConnection();
                string sql = SqlManager.GetSql(SqlQueryType.UPDATEUSER);
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = user.Email;
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = user.Password;
                cmd.Parameters.Add("@role", SqlDbType.Int).Value = role;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                await sqlConnection.OpenAsync();
                int rownumber = await cmd.ExecuteNonQueryAsync();
                if (rownumber < 1)
                    throw new ArgumentException("Update not working");
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
        #endregion

        #region //Categories
        public async Task<List<CategoryDTO>> GetAllCategories()
        {
            SqlConnection sqlConnection = null;
            List<CategoryDTO> categoryList = new List<CategoryDTO>();
            
            try
            {
                sqlConnection = _connectionManager.GetConnection();
                string sql = SqlManager.GetSql(SqlQueryType.READALLCATEGORIES);
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                var row = await cmd.ExecuteReaderAsync();
                while (await row.ReadAsync())
                {
                    categoryList.Add(
                        new CategoryDTO(
                            new CategoryDAO(
                                row.GetInt32(row.GetOrdinal("category_id")),
                                row.GetString(row.GetOrdinal("category_name")),
                                row.IsDBNull(row.GetOrdinal("category_description")) ? null : row.GetString(row.GetOrdinal("category_description"))
                                )
                            )
                        );
                }
                return categoryList;

            } catch (Exception ex)
            {
                throw new ArgumentException("Readl all categories not working!");
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
                sqlConnection = null;
            }

        }

        public async Task<CategoryDTO> GetCategoy(int id)
        {
            CategoryDTO category = null;
            SqlConnection sqlConnection = null;
            try
            {
                string sql = SqlManager.GetSql(SqlQueryType.READCATEGORY);
                sqlConnection = _connectionManager.GetConnection();
                await sqlConnection.OpenAsync();
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.Parameters.Add("id", SqlDbType.Int).Value = id;
                var row = await cmd.ExecuteReaderAsync();
                if (await row.ReadAsync())
                {
                    category = new CategoryDTO(
                        new CategoryDAO(
                            row.GetInt32(row.GetOrdinal("category_id")),
                            row.GetString(row.GetOrdinal("category_name")),
                            row.IsDBNull(row.GetOrdinal("category_description")) ? null : row.GetString(row.GetOrdinal("category_description"))
                            )
                        );
                }
                return category;

            } catch (Exception ex)
            {
                throw new ArgumentException("Read single category not working!");
            } finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
                sqlConnection = null;
            }
        }


        //TODO
        public Task<CategoryDTO> CreateCategory(CategoryDTO category)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDTO> UpdateCategory(int id, CategoryDTO category)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDTO> DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
