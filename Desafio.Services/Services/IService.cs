using Desafio.Domain.Entities;
using Desafio.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Services.Services
{
    public interface IService
    {
        public List<Product> ReadAll();

        public Product Read(int id);

        public string ReadCategory(int id);

        public void Update(int id, Product product);
        public void Create(Product product);

        public void Delete(int id);
    }
}
