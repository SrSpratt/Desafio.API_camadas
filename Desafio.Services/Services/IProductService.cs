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
        Task<List<ProductDto>> ReadAll();

        Task<ProductDto> Read(int id);

        Task Update(int id, ProductDto product);
        Task<int> Create(ProductDto product);

        Task Delete(int id);
    }
}
