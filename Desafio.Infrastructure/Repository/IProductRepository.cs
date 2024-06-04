using Desafio.Domain.Dtos;
using Desafio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Repository
{
    public interface IProductRepository
    {
        Task<int> Create(ProductDTO product);

        Task Update(int id, ProductDTO product);

        Task Delete(int id);

        Task<ProductDTO> Read(int id);
        Task<List<ProductDTO>> ReadAll();
    }
}
