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
                case SqlQueryType.CREATE:
                    sql = "insert into tst_products(Name, Description, SaleValue, Supplier, Value, Category, ExpirationDate) values(@Name, @Description, @SaleValue, @Supplier, @Value, @Category, @ExpirationDate)";
                    break;
                case SqlQueryType.READALL:
                    sql = "select Code, Name, Description, SaleValue, Supplier, Value, Category, ExpirationDate from tst_products";
                    break;
                case SqlQueryType.READ:
                    sql = "select Code, Name, Description, SaleValue, Supplier, Value, Category, ExpirationDate from tst_products where Code = @Code";
                    break;
                case SqlQueryType.UPDATE:
                    sql = "update tst_products set Name = @Name, Description = @Description, SaleValue = @SaleValue, Supplier = @Supplier, Value = @Value, Category = @Category, ExpirationDate = @ExpirationDate where Code = @Code";
                    break;
                case SqlQueryType.DELETE:
                    sql = "delete from tst_products where Code = @Code";
                    break;
            }
            return sql;
        }
    }
}
