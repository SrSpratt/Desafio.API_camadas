using Desafio.Domain.Dtos;
using Desafio.Domain.Entities;
using Desafio.Infrastructure.Repository;

namespace Desafio.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductDTO>> ReadAll()
        {
            List<ProductDTO> dtolist = await _repository.ReadAll();
            return dtolist;
        }

        public async Task<ProductDTO> Read(int id)
        {
            ProductDTO productDto = await _repository.Read(id);
            return productDto;
        }

        public async Task Update(int id, ProductDTO productDto)
        {
            await _repository.Update(id, productDto);
        }

        public async Task<int> Create(ProductDTO productDto)
        {
            return await _repository.Create(productDto);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

    }
}
