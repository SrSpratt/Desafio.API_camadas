using Desafio.Domain.Dtos;
using Desafio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Contexts
{
    public interface IContext
    {
        Task<UserDTO> Login();
        Task<List<Product>> GetAll();

        public Task<Product> Get(int id);

        public Task<string> GetCategory(int name);

        public Task Update(int id, Product product);

        public Task<int> Add(Product product);

        public Task Delete(int id);

        public int NextId();
    }
}
