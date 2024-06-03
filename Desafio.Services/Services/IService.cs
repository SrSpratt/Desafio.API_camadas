using Desafio.Domain.Dtos;
using Desafio.Domain.Entities;
using Desafio.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Services.Services
{
    public interface IService
    {
        Task<List<UserDTO>> ReadUsers();
        Task<int> CreateUser(UserDTO user);

        Task UpdateUser(int id, UserDTO user);
        
        Task<UserDTO> ReadUser(int id);

        Task<UserDTO> Login(string name);

        Task DeleteUser(int id);

        Task<List<ProductDto>> ReadAll();

        Task<ProductDto> Read(int id);

        Task Update(int id, ProductDto product);
        Task<int> Create(ProductDto product);

        Task Delete(int id);
    }
}
