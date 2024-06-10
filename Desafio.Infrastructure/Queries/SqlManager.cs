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
                    sql = @"DELETE FROM tst_users 
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
                                   s.Expiration_date AS 'Expiration Date' 
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
                                   s.Expiration_date AS 'Expiration Date' 
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
                                               WHERE category_name = @OldCategory)";
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
    }
}
