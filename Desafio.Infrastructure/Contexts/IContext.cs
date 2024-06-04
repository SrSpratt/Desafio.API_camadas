using Desafio.Domain.Dtos;
using Desafio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Desafio.Infrastructure.Contexts
{
    public interface IContext
    {
        Task<int> CreateUser(UserDTO user);

        Task UpdateUser(int id, UserDTO user);
        Task<List<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUser(int id);

        Task DeleteUser(int id);

        Task<UserDTO> Login(string name);

        Task<List<ProductDTO>> GetAll();

        public Task<ProductDTO> Get(int id);

        public Task<string> GetCategory(int name);

        public Task Update(int id, ProductDTO product);

        public Task<int> Create(ProductDTO product);

        public Task Delete(int id);

    }
}
