using Desafio.Domain.Entities;
using Desafio.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Services.Services
{
    public class ProductService : IService
    {
        private readonly IRepository _repository;

        public ProductService(IRepository repository)
        {
            _repository = repository;
        }

        public List<Product> ReadAll()
        {
            return _repository.ReadAll();
        }

        public Product Read(int id)
        {
            return _repository.Read(id);
        }

        public void Update(int id, Product product)
        {
            _repository.Update(id, product);
        }

        public void Create(Product product)
        {
            _repository.Create(product);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
