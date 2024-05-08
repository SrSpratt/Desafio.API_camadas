using Desafio.Domain.Entities;
using Desafio.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Repository
{
    public class ProductRepository : IRepository
    {
        private readonly IContext _context;
        public ProductRepository()
        {
            _context = new MockedContext();
        }
        public void Create(Product product)
        {
            int newid = _context.NextId();
            product.Code = newid;
            _context.Add(product);
        }

        public void Delete(int id)
        {
            _context.Delete(id);
        }

        public Product Read(int id)
        {
            return _context.Get(id);
        }

        public List<Product> ReadAll()
        {
            return _context.GetAll();
        }

        public void Update(int id, Product product)
        {
            _context.Update(id, product);
        }
    }
}
