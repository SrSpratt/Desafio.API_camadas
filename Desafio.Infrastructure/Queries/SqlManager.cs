using Desafio.Domain.Enums;

namespace Desafio.Infrastructure.Queries
{
    public class SqlManager
    {
        public static string GetSql(SqlQueryType queryType)
        {
            string sql = string.Empty;

            switch (queryType)
            {
                case SqlQueryType.READALLCATEGORIES:
                    sql = @"SELECT category_id, 
                                   category_name, 
                                   category_description
                            FROM tst_categories";
                    break;
                case SqlQueryType.READCATEGORY:
                    sql = @"SELECT category_id, category_name, category_description 
                                   FROM tst_categories WHERE category_id = @id";
                    break;
                case SqlQueryType.CREATECATEGORY:
                    sql = @"INSERT INTO tst_categories(category_id, category_description, category_name) VALUES (@category_id, @category_description, @category_name)";
                    break;
                case SqlQueryType.DELETECATEGORY:
                    sql = @"DELETE FROM tst_categories 
                            WHERE category_id = @id";
                    break;
                case SqlQueryType.READROLE:
                    sql = @"SELECT role_id 
                                   FROM tst_roles
                            WHERE role_type = @role";
                    break;
                case SqlQueryType.INSERTNAMEUSER:
                    sql = @"INSERT INTO tst_user_names(user_id, user_name) 
                            VALUES(@id, @realname)";
                    break;
                case SqlQueryType.READUSERNAME:
                    sql = @"SELECT a.user_id, 
                                   a.username, 
                                   a.user_email,
                                   a.user_password, 
                                   a.user_role, 
                                   b.role_type, 
                                   b.role_id, 
                                   a.date_registered, 
                                   a.user_registered, 
                                   (SELECT STUFF(
                                        (SELECT ' ' + c.user_name FROM 
                                            tst_user_names c
                                         WHERE c.user_id = a.user_id
                                         FOR XML PATH(''), TYPE).value(
                                                      '.', 'NVARCHAR(MAX)'), 1, 1, '')) AS concatenated_user_names
                            FROM tst_users a JOIN tst_roles b ON a.user_role = b.role_id WHERE username = @name";
                    break;
                case SqlQueryType.READUSER:
                    sql = @"SELECT a.user_id, 
                                   a.username, 
                                   a.user_email,
                                   a.user_password, 
                                   a.user_role, 
                                   b.role_type, 
                                   b.role_id, 
                                   a.date_registered, 
                                   a.user_registered, 
                                   (SELECT STUFF(
                                        (SELECT ' ' + c.user_name FROM 
                                            tst_user_names c
                                         WHERE c.user_id = a.user_id
                                         FOR XML PATH(''), TYPE).value(
                                                      '.', 'NVARCHAR(MAX)'), 1, 1, '')) AS concatenated_user_names
                            FROM tst_users a JOIN tst_roles b ON a.user_role = b.role_id WHERE user_id = @user_id";
                    break;
                case SqlQueryType.READUSERS:
                    sql = @"SELECT a.user_id, 
                                   a.username, 
                                   a.user_email,
                                   a.user_password, 
                                   a.user_role, 
                                   b.role_type, 
                                   b.role_id, 
                                   a.date_registered, 
                                   a.user_registered, 
                                   (SELECT STUFF(
                                        (SELECT ' ' + c.user_name FROM 
                                            tst_user_names c
                                         WHERE c.user_id = a.user_id
                                         FOR XML PATH(''), TYPE).value(
                                                      '.', 'NVARCHAR(MAX)'), 1, 1, '')) AS concatenated_user_names
                            FROM tst_users a JOIN tst_roles b ON a.user_role = b.role_id";
                    break;
                case SqlQueryType.CREATEUSER:
                    sql = @"INSERT INTO tst_users(username, user_email, user_password, date_registered, user_role, user_registered) 
                            OUTPUT INSERTED.user_id 
                            VALUES(@username, @email, @password, GETDATE(), @role, @user)";
                    break;
                case SqlQueryType.UPDATEUSER:
                    sql = @"UPDATE tst_users SET
                                user_email = @email, 
                                user_password = @password, 
                                user_role = @role 
                            WHERE user_id = @id";
                    break;
                case SqlQueryType.DELETEUSER:
                    sql = @"DELETE FROM tst_user_names 
                            WHERE user_id = @id;
                            DELETE FROM tst_users 
                            WHERE user_id = @id";
                    break;
                case SqlQueryType.READNAME: //Isso deve ficar como readmaincategory
                    sql = @"SELECT category_name AS 'Category' 
                                   FROM tst_categories c 
                            JOIN tst_product_category pc ON pc.category_id = c.category_id 
                            WHERE product_id = @Code";
                    break;
                case SqlQueryType.CREATE:
                    sql = @"DECLARE @output_table TABLE (code int, stock_id int)
                            INSERT INTO tst_products(Name, Description) 
                                   OUTPUT INSERTED.Code INTO @output_table(code) 
                            VALUES (@name, @description)
                            INSERT INTO tst_stock(Sale_value, Purchase_value, Amount, Product_id, Supplier, Expiration_date) 
                                   OUTPUT INSERTED.stock_id INTO @output_table(stock_id) 
                            VALUES (@salevalue, @purchasevalue, @amount, (SELECT TOP 1 code 
                                                                                 FROM @output_table), @supplier, @expirationdate)
                            INSERT INTO tst_product_category(product_id, category_id) 
                            VALUES ((SELECT TOP 1 code 
                                            FROM @output_table),(SELECT category_id 
                                                                        FROM tst_categories 
                                                                 WHERE category_name = @category))
                            INSERT INTO tst_stock_updates(stock_id, operation_type, operation_date, operation_user, operation_amount) 
                            VALUES ((SELECT TOP 1 stock_id 
                                            FROM @output_Table where stock_id is not null), @operation, GETDATE(), (SELECT username 
                                                                                                                           FROM tst_users 
                                                                                                                    WHERE username = @username), @operationamount)
                            SELECT TOP 1 stock_id 
                                   FROM @output_table 
                            WHERE stock_id is not null;";
                    break;
                case SqlQueryType.READALL:
                    sql = @"SELECT p.Code AS 'Code', 
                                   p.Name AS 'Name', 
                                   p.Description AS 'Description', 
                                   c.category_name AS 'Category', 
                                   s.Sale_value AS 'Value', 
                                   s.Amount AS 'Amount', 
                                   s.Purchase_value AS 'Purchase Value', 
                                   s.Supplier AS 'Supplier', 
                                   s.Expiration_date AS 'Expiration Date',
                                   s.stock_id AS 'Stock'
                           FROM tst_products p 
                           JOIN tst_stock s ON p.Code = s.Product_id 
                           JOIN tst_product_category aux ON p.Code = aux.product_id 
                           JOIN tst_categories c ON c.category_id = aux.category_id 
                           ORDER BY p.Name ASC";
                    break;
                case SqlQueryType.READ:
                    sql = @"SELECT p.Code AS 'Code', 
                                   p.Name AS 'Name', 
                                   p.Description AS 'Description', 
                                   c.category_name AS 'Category', 
                                   s.Sale_value AS 'Value', 
                                   s.Amount AS 'Amount', 
                                   s.Purchase_value AS 'Purchase Value', 
                                   s.Supplier AS 'Supplier', 
                                   s.Expiration_date AS 'Expiration Date',
                                   s.stock_id AS 'Stock'
                           FROM tst_products p 
                           JOIN tst_stock s ON p.Code = s.Product_id 
                           JOIN tst_product_category aux ON p.Code = aux.product_id 
                           JOIN tst_categories c ON c.category_id = aux.category_id 
                           WHERE p.Code = @Code";
                    break;
                case SqlQueryType.UPDATE:
                    sql = @"UPDATE tst_products SET 
                                   Name = @Name, 
                                   Description = @Description 
                            WHERE Code = @Code;
                            UPDATE tst_stock SET 
                                   Sale_value = @SaleValue, 
                                   Purchase_value = @Value, 
                                   Amount = @Amount, 
                                   Expiration_date = @ExpirationDate 
                            WHERE Product_id = @Code;
                            UPDATE tst_product_category SET 
                                   product_id = @Code, 
                                   category_id = (SELECT category_id FROM 
                                                         tst_categories 
                                                  WHERE category_name = @Category) 
                            WHERE product_id = @Code 
                            AND category_id = (SELECT category_id FROM 
                                                      tst_categories 
                                               WHERE category_name = @OldCategory)
                            INSERT INTO tst_stock_updates(stock_id, operation_type, operation_date, operation_user, operation_amount) 
                            VALUES (@stock_id, @operation, GETDATE(), @user, @operationamount);";
                    break;
                case SqlQueryType.DELETE:
                    sql = @"DELETE FROM tst_stock 
                            WHERE Product_id = @Code;
                            DELETE FROM tst_product_category 
                            WHERE product_id = @Code;
                            DELETE FROM tst_products 
                            WHERE Code = @Code;";
                    break;
                case SqlQueryType.READOPERATIONS:
                    sql = @"SELECT * FROM
	                               tst_stock_updates 
                            WHERE stock_id = @id";
                    break;

            }
            return sql;
        }

        public static string ChooseSql(SqlQueryType type)
        {
            string sql = string.Empty;
            switch (type)
            {
                case SqlQueryType.P_READALL:
                    sql = @"SELECT Code, Name, Description
                            FROM tst_products;";
                    break;
                case SqlQueryType.P_READ:
                    sql = @"SELECT Code, Name, Description
                            FROM tst_products 
                            WHERE Code = @code;";
                    break;
                case SqlQueryType.P_CREATE:
                    sql = @"INSERT INTO tst_products
                            (Name, Description)
                            OUTPUT INSERTED.Code
                            VALUES(@Name, @Description);";
                    break;
                case SqlQueryType.P_UPDATE:
                    sql = @"UPDATE tst_products
                            SET Name = @name, Description=@description
                            OUTPUT DELETED.Name
                            WHERE Code=@code;";
                    break;
                case SqlQueryType.P_DELETE:
                    sql = @"DELETE FROM tst_products
                            WHERE Code=@code;";
                    break;
                case SqlQueryType.S_READALL:
                    sql = @"SELECT Product_id, Amount, Purchase_value, Sale_value, Stock_id, Supplier, Expiration_date
                            FROM tst_stock;";
                    break;
                case SqlQueryType.S_READ:
                    sql = @"SELECT Product_id, Amount, Purchase_value, Sale_value, Stock_id, Supplier, Expiration_date
                            FROM tst_stock
                            WHERE Product_id = @product_id;";
                    break;
                case SqlQueryType.S_CREATE:
                    sql = @"INSERT INTO tst_stock
                            (Product_id, Amount, Purchase_value, Sale_value, Supplier, Expiration_date)
                            OUTPUT INSERTED.Stock_id
                            VALUES(@product_id, @amount, @purchase_value, @sale_value, @supplier, @expiration_date);";
                    break;
                case SqlQueryType.S_UPDATE:
                    sql = @"UPDATE tst_stock
                            SET Amount=@amount, Purchase_value=@purchase_value, Sale_value=@sale_value, Product_id=@product_id Supplier=@supplier, Expiration_date=@expiration_date
                            OUTPUT.product_id
                            WHERE Stock_id=@stock_id;";
                    break;
                case SqlQueryType.S_DELETE:
                    sql = @"DELETE FROM tst_stock
                            WHERE Product_id=@product_id;";
                    break;
                case SqlQueryType.C_READALL:
                    sql = @"SELECT category_id, category_name, category_description
                            FROM tst_categories;";
                    break;
                case SqlQueryType.C_READ:
                    sql = @"SELECT category_id, category_name, category_description
                            FROM tst_categories;
                            WHERE category_id = @category_id";
                    break;
                case SqlQueryType.C_CREATE:
                    sql = @"INSERT INTO tst_categories
                            (category_name, category_description)
                            OUTPUT INSERTED.category_id
                            VALUES(@category_name, @category_description);";
                    break;
                case SqlQueryType.C_UPDATE:
                    sql = @"UPDATE tst_categories
                            SET category_name='', category_description=''
                            OUTPUT DELETED.category_name
                            WHERE category_id=@category_id;";
                    break;
                case SqlQueryType.C_DELETE:
                    sql = @"DELETE FROM tst_categories
                            WHERE category_id=@category_id;";
                    break;
                case SqlQueryType.U_READALL:
                    sql = @"SELECT user_id, username, user_email, user_password, date_registered, user_role, user_registered
                            FROM tst_users;";
                    break;
                case SqlQueryType.U_READ:
                    sql = @"SELECT user_id, username, user_email, user_password, date_registered, user_role, user_registered
                            FROM tst_users
                            WHERE user_id=@user_id;";
                    break;
                case SqlQueryType.U_CREATE:
                    sql = @"INSERT INTO tst_users
                            (username, user_email, user_password, date_registered, user_role, user_registered)
                            OUTPUT INSERTED.user_id
                            VALUES(@username, @user_email, @user_password, @date_registered, @user_role, @user_registered);";
                    break;
                case SqlQueryType.U_UPDATE:
                    sql = @"UPDATE tst_users
                            SET user_email=@user_email, user_password=@user_password, date_registered=@date_registered, user_role=@user_role, user_registered=@user_registered
                            OUTPUT DELETED.username
                            WHERE user_id=@user_id;";
                    break;
                case SqlQueryType.U_DELETE:
                    sql = @"DELETE FROM tst_users
                            WHERE user_id=@user_id;";
                    break;
                case SqlQueryType.R_READALL:
                    sql = @"SELECT role_id, role_type
                            FROM tst_roles;";
                    break;
                case SqlQueryType.R_READ:
                    sql = @"SELECT role_id, role_type
                            FROM tst_roles
                            WHERE role_id=@role_id;";
                    break;
                case SqlQueryType.R_CREATE:
                    sql = @"INSERT INTO tst_roles
                            (role_type)
                            OUTPUT INSERTED.role_id
                            VALUES(@role_type);";
                    break;
                case SqlQueryType.R_UPDATE:
                    sql = @"UPDATE tst_roles
                            SET role_type=''
                            OUTPUT DELETED.role_id
                            WHERE role_id=@role_id;";
                    break;
                case SqlQueryType.R_DELETE:
                    sql = @"DELETE FROM tst_roles
                            WHERE role_id=@role_id;";
                    break;
                case SqlQueryType.O_READALL:
                    sql = @"SELECT operation_id, stock_id, operation_type, operation_date, operation_user, operation_amount
                            FROM tst_stock_updates;";
                    break;
                case SqlQueryType.O_READ:
                    sql = @"SELECT operation_id, stock_id, operation_type, operation_date, operation_user, operation_amount
                            FROM tst_stock_updates
                            WHERE stock_id=@stock_id;";
                    break;
                case SqlQueryType.O_CREATE:
                    sql = @"INSERT INTO tst_stock_updates
                            (stock_id, operation_type, operation_date, operation_user, operation_amount)
                            VALUES(@stock_id, @operation_type, @operation_date, @operation_user, @operation_amount);";
                    break;
                case SqlQueryType.O_UPDATE:
                    sql = @"UPDATE tst_stock_updates
                            SET stock_operation=@stock_operation, operation_type=@operation_type, operation_date=@operation_date, operation_user=@operation_user, operation_amount=@operation_amount
                            WHERE operation_id=@operation_id;";
                    break;
                case SqlQueryType.O_DELETE:
                    sql = @"DELETE FROM tst_stock_updates
                            WHERE operation_id=@operation_id;";
                    break;
                case SqlQueryType.PC_READALL:
                    sql = @"SELECT product_id, category_id
                            FROM tst_product_category;";
                    break;
                case SqlQueryType.PC_READ:
                    sql = @"SELECT product_id, category_id
                            FROM tst_product_category;
                            WHERE product_id=@product_id";
                    break;
                case SqlQueryType.PC_CREATE:
                    sql = @"INSERT INTO tst_product_category
                            (product_id, category_id)
                            VALUES(@product_id, @category_id);";
                    break;
                case SqlQueryType.PC_UPDATE:
                    sql = @"UPDATE tst_product_category
                            SET  
                            WHERE product_id=@product_id AND category_id=@category_id;";
                    break;
                case SqlQueryType.PC_DELETE:
                    sql = @"DELETE FROM tst_product_category
                            WHERE product_id=@product_id AND category_id=@category_id;";
                    break;
                case SqlQueryType.UN_READALL:
                    sql = @"SELECT user_name_id, user_id, user_name
                            FROM tst_user_names;";
                    break;
                case SqlQueryType.UN_READ:
                    sql = @"SELECT user_name_id, user_id, user_name
                            FROM tst_user_names
                            WHERE user_id=@user_id;";
                    break;
                case SqlQueryType.UN_CREATE:
                    sql = @"INSERT INTO tst_user_names
                            (user_id, user_name)
                            VALUES(@user_id, @user_name);";
                    break;
                case SqlQueryType.UN_UPDATE:
                    sql = @"UPDATE tst_user_names
                            SET user_id=0, user_name=''
                            WHERE user_name_id=0;";
                    break;
                case SqlQueryType.UN_DELETE:
                    sql = @"DELETE FROM tst_user_names
                            WHERE user_id=@user_id;";
                    break;


            }

            return sql;
        }
    }
}