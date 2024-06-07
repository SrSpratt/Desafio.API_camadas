using Desafio.Domain.Dtos;

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
