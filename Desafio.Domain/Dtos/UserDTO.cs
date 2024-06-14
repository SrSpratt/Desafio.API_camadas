using Desafio.Domain.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Desafio.Domain.Dtos
{
    public class UserDTO
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public DateTime DateRegistered { get; set; }

        public string UserRegistered { get; set; }

        public string RealName { get; set; } 

        public UserDTO()
        {

        }

        public UserDTO(int id, string name, string email, string password, string role, DateTime dateRegistered, string userRegistered)
        {
            ID = id;
            Name = name;
            Email = email;
            Password = password;
            Role = role;
            DateRegistered = dateRegistered;
            UserRegistered = userRegistered;
        }

        public UserDTO(UserDAO user, RoleDAO role)
        {
            ID = user.ID;
            Name = user.Name;
            Email = user.Email;
            Password = user.Password;
            DateRegistered = user.DateRegistered;
            UserRegistered = user.UserRegistered;
            RealName = user.RealName;
            Role = role.Type;
        }
    }
}
