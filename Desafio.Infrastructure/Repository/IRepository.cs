using Desafio.Domain.Dtos;
using Desafio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Repository
{
    public interface IRepository
    {
        Task<int> CreateUser(UserDTO user);

        Task UpdateUser(int id, UserDTO user);
        Task<List<UserDTO>> ReadUsers();
        Task<UserDTO> ReadUser(int id);

        Task<UserDTO> Login(string name);

        Task DeleteUser(int id);

        Task<int> Create(Product product);

        Task Update(int id, Product product);

        Task Delete(int id);

        Task<Product> Read(int id);
        Task<List<Product>> ReadAll();
    }
}
