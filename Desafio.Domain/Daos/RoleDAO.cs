using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Desafio.Domain.Daos
{
    public class RoleDAO
    {
        public int ID { get; set; }

        public string Type {  get; set; }

        public static Dictionary<string, string> DAOMap = new Dictionary<string, string>
        {
            {"role_id", nameof(ID) },
            {"category_type", nameof(Type) }
        };

        public static Dictionary<string, string> DBMap = new Dictionary<string, string>
        {
            {nameof(ID), "role_id"},
            {nameof(Type), "role_type"}
        };
    }

}
