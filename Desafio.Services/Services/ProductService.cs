using Desafio.Domain.Dtos;
using Desafio.Domain.Entities;
using Desafio.Infrastructure.Repository;

namespace Desafio.Services.Services
{
    public class ProductService : IService
    {
        private readonly IRepository _repository;

        public ProductService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductDto>> ReadAll()
        {
            List<Product> list = await _repository.ReadAll();
            List<ProductDto> dtolist = list != null ? Product.ToDtoList(list) : null;
            return dtolist;
        }

        public async Task<ProductDto> Read(int id)
        {
            Product product = await _repository.Read(id);
            ProductDto productDto = product != null ? product.ToDto() : null;
            return productDto;
        }

        public async Task Update(int id, ProductDto productDto)
        {
            Product product = productDto.ToEntity();
            await _repository.Update(id, product);
        }

        public async Task<int> Create(ProductDto productDto)
        {
            Product product = productDto.ToEntity();
            return await _repository.Create(product);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<UserDTO> Login(string username)
        {
            return await _repository.Login(username);
        }
    }
}
