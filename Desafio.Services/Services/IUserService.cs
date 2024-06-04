using Desafio.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Services.Services
{
    public interface IUserService
    {
        Task<List<UserDTO>> ReadUsers();
        Task<int> CreateUser(UserDTO user);

        Task UpdateUser(int id, UserDTO user);

        Task<UserDTO> ReadUser(int id);

        Task<LoginResponse> Login(string name, string Password);

        Task DeleteUser(int id);
    }
}
