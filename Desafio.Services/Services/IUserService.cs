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
        Task<List<UserDTO>> ReadAll();
        Task<int> Create(UserDTO user);

        Task Update(int id, UserDTO user);

        Task<UserDTO> Read(int id);

        Task<LoginResponse> Login(string name, string Password);

        Task Delete(int id);
    }
}
