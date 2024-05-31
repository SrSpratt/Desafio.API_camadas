using Desafio.Domain.Dtos;
using Desafio.Domain.Entities;
using Desafio.Domain.Enums;
using Desafio.Domain.Setup;
using Desafio.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Repository
{
    public class ProductRepository : IRepository
    {
        private readonly IContext _context;
        private readonly IApiConfig _apiConfig;
        public ProductRepository(IApiConfig apiConfig)
        {
            _apiConfig = apiConfig;
            _context = new SqlContext(_apiConfig);
        }
        public async Task<int> Create(Product product)
        {
            return await _context.Add(product);
        }

        public async Task Delete(int id)
        {
            await _context.Delete(id);
        }

        public async Task<Product> Read(int id)
        {
            return await _context.Get(id);
        }

        public async Task<List<Product>> ReadAll()
        {
            return await _context.GetAll();
        }

        public async Task Update(int id, Product product)
        {
            await _context.Update(id, product);
        }

        public async Task<UserDTO> Login(string username)
        {
            return await _context.Login(username);
        }
    }
}
