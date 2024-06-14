using Desafio.Domain.Daos;
using Desafio.Domain.Dtos;
using Desafio.Domain.Setup;
using Desafio.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SqlContext<CategoryDAO> _context;
        private readonly IApiConfig _config;
        public CategoryRepository(IApiConfig config) 
        {
            _config = config;
            _context = new SqlContext<CategoryDAO>(config);
        }
        public async Task<List<CategoryDAO>> GetAll()
        {
            return await _context.ReadAll();
        }

        public async Task<CategoryDAO> Get(int id)
        {
            return await _context.Read(id);
        }

        public async Task<CategoryDAO> Create(CategoryDAO category)
        {
            return await _context.Place(category);
        }

        public async Task<CategoryDAO> Update(int id, CategoryDAO category)
        {
            return await _context.Replace(category);
        }

        public async Task<CategoryDAO> Delete(int id)
        {
            return await _context.Remove(id);
        }
    }
}
