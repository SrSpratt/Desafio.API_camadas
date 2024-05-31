using Desafio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Queries
{
    public class SqlManager
    {
        public static string GetSql(SqlQueryType queryType)
        {
            string sql = string.Empty;

            switch (queryType)
            {
                case SqlQueryType.READUSER:
                    sql = "SELECT user_id, username, user_email, user_password, user_role, role_type, role_id FROM tst_users JOIN tst_roles ON user_role = role_id WHERE username = @username";
                    break;
                case SqlQueryType.READNAME:
                    sql = "SELECT category_name AS 'Category' FROM tst_categories c JOIN tst_product_category pc ON pc.category_id = c.category_id WHERE product_id = @Code";
                    break;
                case SqlQueryType.NEWCREATE:
                    sql = "INSERT INTO tst_products(Name, Description) OUTPUT INSERTED.Code VALUES (@Name, @Description);" +
                        "INSERT INTO tst_stock(Sale_value, Purchase_value, Amount, Product_id, Supplier, Expiration_date) VALUES (@SaleValue, @Value, @Amount, (SELECT Code FROM tst_products WHERE Name = @Name), @Supplier, @ExpirationDate);" +
                        "INSERT INTO tst_product_category(product_id, category_id) VALUES ((SELECT Code FROM tst_products WHERE Name = @Name),(SELECT category_id FROM tst_categories WHERE category_name = @Category));";
                    break;
                case SqlQueryType.NEWREADALL: //esse
                    sql = "SELECT p.Code AS 'Code', p.Name AS 'Name', p.Description AS 'Description', c.category_name AS 'Category', s.Sale_value AS 'Value', s.Amount AS 'Amount', s.Purchase_value AS 'Purchase Value', s.Supplier AS 'Supplier', s.Expiration_date AS 'Expiration Date' FROM tst_products p JOIN tst_stock s ON p.Code = s.Product_id JOIN tst_product_category aux ON p.Code = aux.product_id JOIN tst_categories c ON c.category_id = aux.category_id ORDER BY p.Name ASC";
                    break;
                case SqlQueryType.NEWREAD:
                    sql = "SELECT p.Code AS 'Code', p.Name AS 'Name', p.Description AS 'Description', c.category_name AS 'Category', s.Sale_value AS 'Value', s.Amount AS 'Amount', s.Purchase_value AS 'Purchase Value', s.Supplier AS 'Supplier', s.Expiration_date AS 'Expiration Date' FROM tst_products p JOIN tst_stock s ON p.Code = s.Product_id JOIN tst_product_category aux ON p.Code = aux.product_id JOIN tst_categories c ON c.category_id = aux.category_id WHERE p.Code = @Code";
                    break;
                case SqlQueryType.NEWUPDATE:
                    sql = "UPDATE tst_products SET Name = @Name, Description = @Description WHERE Code = @Code;" +
                        "UPDATE tst_stock SET Sale_value = @SaleValue, Purchase_value = @Value, Amount = @Amount, Expiration_date = @ExpirationDate WHERE Product_id = @Code;" +
                        "UPDATE tst_product_category SET product_id = @Code, category_id = (SELECT category_id FROM tst_categories WHERE category_name = @Category) WHERE product_id = @Code AND category_id = (SELECT category_id FROM tst_categories WHERE category_name = @OldCategory)"; // Funciona para quando cada produto tem apenas uma categoria, tem que refatorar para o caso de um produto acessar mais categorias
                    break;
                case SqlQueryType.NEWDELETE:
                    sql = "DELETE FROM tst_stock WHERE Product_id = @Code;" +
                    "DELETE FROM tst_product_category WHERE product_id = @Code;" +
                    "DELETE FROM tst_products WHERE Code = @Code;" ;
                    break;

            }
            return sql;
        }
    }
}
