using Desafio.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Repository
{
    public interface IUserRepository
    {
        Task<int> CreateUser(UserDTO user);

        Task UpdateUser(int id, UserDTO user);
        Task<List<UserDTO>> ReadUsers();
        Task<UserDTO> ReadUser(int id);

        Task<UserDTO> Login(string name);

        Task DeleteUser(int id);
    }
}
