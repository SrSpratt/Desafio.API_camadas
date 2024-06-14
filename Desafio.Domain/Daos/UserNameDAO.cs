using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Desafio.Domain.Daos
{
    public class UserNameDAO
    {
        public int UserNameID { get; set; }
        public int UserID { get; set; }

        public string UserName { get; set; }

        public static Dictionary<string, string> DAOMap = new Dictionary<string, string>
        {
            {"user_name_id", nameof(UserNameID) },
            {"user_id", nameof(UserID) },
            {"user_name", nameof(UserName) }
        };

        public static Dictionary<string, string> DBMap = new Dictionary<string, string>
        {
            {nameof(UserNameID), "user_name_id"},
            {nameof(UserID), "user_id"},
            {nameof(UserName), "user_name"}
        };
    }
}
