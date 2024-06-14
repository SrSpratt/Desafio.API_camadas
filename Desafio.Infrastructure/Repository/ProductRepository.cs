using Desafio.Domain.Dtos;
using Desafio.Domain.Setup;
using Desafio.Infrastructure.Contexts;

namespace Desafio.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IContext _context;
        private readonly IApiConfig _apiConfig;
        public ProductRepository(IApiConfig apiConfig)
        {
            _apiConfig = apiConfig;
            _context = new SqlContext<ProductDTO>(_apiConfig);
        }
        public async Task<int> Create(ProductDTO product)
        {
            return await _context.Create(product);
        }

        public async Task Delete(int id)
        {
            await _context.Delete(id);
        }

        public async Task<ProductDTO> Read(int id)
        {
            return await _context.Get(id);
        }

        public async Task<List<ProductDTO>> ReadAll()
        {
            return await _context.GetAll();
        }

        public async Task Update(int id, ProductDTO product)
        {
            await _context.Update(id, product);
        }
    }
}
