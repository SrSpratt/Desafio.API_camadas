using Desafio.Domain.Entities;
using Desafio.Domain.Enums;
using Desafio.Domain.Setup;
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
        private readonly IApiConfig _apiConfig;
        public ProductRepository(IApiConfig apiConfig)
        {
            _apiConfig = apiConfig;
            if (Settings.SELECTED_DATABASE == DatabaseType.Volatile)
                _context = new MockedContext();
            else
                _context = new SqlContext(_apiConfig);
        }
        public void Create(Product product)
        {
            if (Settings.SELECTED_DATABASE == DatabaseType.Volatile)
            {
                int newid = _context.NextId();
                product.Code = newid;
            }
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

        public Product ReadName(string name)
        {
            return _context.GetName(name);
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
