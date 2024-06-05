using Desafio.Domain.Dtos;
using Desafio.Domain.Setup;
using Desafio.Infrastructure.Contexts;
using Desafio.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<CategoryDTO>> GetAllCategories()
        {
            return await _repository.GetAllCategories();
        }

        public async Task<CategoryDTO> GetCategoy(int id)
        {
            return await _repository.GetCategoy(id);
        }

        public async Task<CategoryDTO> CreateCategory(CategoryDTO category)
        {
            return await _repository.CreateCategory(category);
        }

        public async Task<CategoryDTO> UpdateCategory(int id, CategoryDTO category)
        {
            return await _repository.UpdateCategory(id, category);
        }

        public async Task<CategoryDTO> DeleteCategory(int id)
        {
            return await _repository.DeleteCategory(id);
        }
    }
}
