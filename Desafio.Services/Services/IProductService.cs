using Desafio.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Services.Services
{
    public interface IProductService
    {        
        Task<List<ProductDTO>> ReadAll();

        Task<ProductDTO> Read(int id);

        Task Update(int id, ProductDTO product);
        Task<int> Create(ProductDTO product);

        Task Delete(int id);
    }
}
