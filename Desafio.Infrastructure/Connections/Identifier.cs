using Desafio.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Connections
{
    public static class Identifier
    {
        public static bool IsOfType<T>(string classname)
        {
            string typeName = typeof(T).Name;

            return typeName.Equals(classname);
        }

        public static SqlQueryType GetCommandType<T>(string method)
        {
            string type = typeof(T).Name;
            char prefix = type[0];
            string concat = type[0] + "_" + method;

            return (SqlQueryType)Enum.Parse(typeof(SqlQueryType), concat);
        }

        public static Dictionary<Type, SqlDbType> SqlTypeMap = new Dictionary<Type, SqlDbType>
        {
            {typeof(string), SqlDbType.VarChar},
            {typeof(int), SqlDbType.Int},
            {typeof(DateTime), SqlDbType.DateTime},
            {typeof(double), SqlDbType.Real}
        };
    }
}
