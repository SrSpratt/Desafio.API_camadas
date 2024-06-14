using Desafio.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Services.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAll();

        Task<CategoryDTO> Get(int id);

        Task<CategoryDTO> Create(CategoryDTO category);

        Task<CategoryDTO> Update(int id, CategoryDTO category);

        Task<CategoryDTO> Delete(int id);
    }
}
