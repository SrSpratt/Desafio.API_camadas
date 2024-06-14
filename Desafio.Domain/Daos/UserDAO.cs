using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.Daos
{
    public class UserDAO
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime DateRegistered { get; set; }

        public string UserRegistered { get; set; }

        public string RealName { get; set; }

        public string Role {  get; set; }

        public static Dictionary<string, string> DAOMap = new Dictionary<string, string>
        {
            {"user_id", nameof(ID) },
            {"username", nameof(Name) },
            {"user_email", nameof(Email) },
            {"username", nameof(Name) },
            {"user_password", nameof(Password) },
            {"user_name", nameof(RealName) },
            {"date_registered", nameof(DateRegistered) },
            {"user_registered", nameof(UserRegistered) },
            {"user_role", nameof(Role) }
        };

        public static Dictionary<string, string> DBMap = new Dictionary<string, string>
        {
            {nameof(ID), "user_id"},
            {nameof(Name), "username"},
            {nameof(Email), "user_email"},
            {nameof(Name) , "username"},
            {nameof(Password), "user_password"},
            {nameof(RealName), "user_name"},
            {nameof(DateRegistered), "date_registered"},
            {nameof(UserRegistered), "user_registered"},
            {nameof(Role), "user_role"}
        };
    }
}
