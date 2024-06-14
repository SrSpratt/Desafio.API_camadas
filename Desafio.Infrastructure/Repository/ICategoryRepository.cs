using Desafio.Domain.Daos;
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
        Task<List<CategoryDAO>> GetAll();

        Task<CategoryDAO> Get(int id);

        Task<CategoryDAO> Create(CategoryDAO category);

        Task<CategoryDAO> Update(int id, CategoryDAO category);

        Task<CategoryDAO> Delete(int id);
    }
}
