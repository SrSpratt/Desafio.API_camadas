using Desafio.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Repository
{
    public interface ICategoryRepository
    {
        Task<List<CategoryDTO>> GetAllCategories();

        Task<CategoryDTO> GetCategoy(int id);

        Task<CategoryDTO> CreateCategory(CategoryDTO category);

        Task<CategoryDTO> UpdateCategory(int id, CategoryDTO category);

        Task<CategoryDTO> DeleteCategory(int id);
    }
}
