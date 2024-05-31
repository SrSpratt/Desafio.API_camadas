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
        public Task<UserDTO> Login(string username);
        public Task<List<Product>> ReadAll();

        public Task<Product> Read(int id);

        public Task<string> ReadCategory(int id);

        public Task Update(int id, Product product);
        public Task<int> Create(Product product);

        public Task Delete(int id);
    }
}
