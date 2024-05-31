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
    public class ProductService : IService
    {
        private readonly IRepository _repository;

        public ProductService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Product>> ReadAll()
        {
            return await _repository.ReadAll();
        }

        public async Task<Product> Read(int id)
        {
            return await _repository.Read(id);
        }

        public async Task<string> ReadCategory(int id)
        {
            return await _repository.ReadCategory(id);
        }

        public async Task Update(int id, Product product)
        {
            await _repository.Update(id, product);
        }

        public async Task<int> Create(Product product)
        {
            return await _repository.Create(product);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<UserDTO> Login(string username)
        {
            return await _repository.Login(username);
        }
    }
}
