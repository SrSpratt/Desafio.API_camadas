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
        private readonly IContext _context;
        private readonly IApiConfig _config;
        public CategoryRepository(IApiConfig config) 
        {
            _config = config;
            _context = new SqlContext(config);
        }
        public async Task<List<CategoryDTO>> GetAllCategories()
        {
            return await _context.GetAllCategories();
        }

        public async Task<CategoryDTO> GetCategoy(int id)
        {
            return await _context.GetCategoy(id);
        }

        public async Task<CategoryDTO> CreateCategory(CategoryDTO category)
        {
            return await _context.CreateCategory(category);
        }

        public async Task<CategoryDTO> UpdateCategory(int id, CategoryDTO category)
        {
            return await _context.UpdateCategory(id, category);
        }

        public async Task<CategoryDTO> DeleteCategory(int id)
        {
            return await _context.DeleteCategory(id);
        }
    }
}
