using Desafio.Domain.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


        public UserDTO(UserDAO user, RoleDAO role)
        {
            ID = user.ID;
            Name = user.Name;
            Email = user.Email;
            Password = user.Password;
            Role = role.Type;
        }
    }
}
