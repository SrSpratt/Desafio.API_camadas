using Desafio.Domain.Daos;
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
        public async Task<List<CategoryDTO>> GetAll()
        {
            List<CategoryDTO> categoryList = new List<CategoryDTO>();

            var daoList = await _repository.GetAll();
            foreach (var item in daoList)
                categoryList.Add(
                    new CategoryDTO(item)
                    );
            return categoryList;
        }

        public async Task<CategoryDTO> Get(int id)
        {
            return new CategoryDTO( 
                await _repository.Get(id)
                );
        }

        public async Task<CategoryDTO> Create(CategoryDTO category)
        {
            CategoryDAO categoryDAO = new CategoryDAO()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            };
            return new CategoryDTO(
                await _repository.Create(categoryDAO)
                );
        }

        public async Task<CategoryDTO> Update(int id, CategoryDTO category)
        {
            CategoryDAO categoryDAO = new CategoryDAO()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            };
            return new CategoryDTO(
                await _repository.Update(id, categoryDAO)
                );
        }

        public async Task<CategoryDTO> Delete(int id)
        {
            return new CategoryDTO(
                await _repository.Delete(id)
                );
        }
    }
}
