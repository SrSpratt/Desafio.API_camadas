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
        Task<int> Create(Product product);

        Task Update(int id, Product product);

        Task Delete(int id);

        Task<Product> Read(int id);
        Task<List<Product>> ReadAll();
    }
}
