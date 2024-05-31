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
        public Task<UserDTO> Login(string username);
        public Task<int> Create(Product product);

        public Task Update(int id, Product product);

        public Task Delete(int id);

        public Task<Product> Read(int id);

        public Task<string> ReadCategory(int id);

        public Task<List<Product>> ReadAll();
    }
}
